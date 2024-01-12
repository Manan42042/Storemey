using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using Abp.UI;
using Storemey.StoreTax.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.StoreTaxGroups;
using Storemey.StoreTaxGroupLinks;

namespace Storemey.StoreTax
{
    [AbpAuthorize]
    public class StoreTaxAppService : AbpServiceBase, IStoreTaxAppService
    {
        private readonly IStoreTaxManager _StoreTaxManager;
        private readonly IStoreTaxGroupLinksManager _StoreTaxGroupsManager;
        private readonly IRepository<StoreTax, Guid> _StoreTaxRepository;

        public StoreTaxAppService(
            IStoreTaxManager StoreTaxManager,
             IStoreTaxGroupLinksManager StoreTaxGroupsManager,
        IRepository<StoreTax, Guid> StoreTaxRepository)
        {
            _StoreTaxManager = StoreTaxManager;
            _StoreTaxRepository = StoreTaxRepository;
            _StoreTaxGroupsManager = StoreTaxGroupsManager;
        }

        public async Task<ListResultDto<GetStoreTaxOutputDto>> ListAll()
        {
            var events = await _StoreTaxManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxOutputDto>>();
            return new ListResultDto<GetStoreTaxOutputDto>(returnData);
        }


        public async Task<List<GetStoreTaxOutputDto>> GetByGroupId(int groupid)
        {
            var events = await _StoreTaxManager.ListAll();
            var data = _StoreTaxGroupsManager.GetByGroupID(groupid);
            var returnData = events.MapTo<List<GetStoreTaxOutputDto>>();
            foreach (var item in returnData)
            {
                item.Isassign = data != null && data.Result.Any(x => x.TaxId == item.Id) ? true : false;
            }
            return returnData;
        }


        public async Task Create(CreateStoreTaxInputDto input)
        {

            try
            {

                var mapData = input.MapTo<StoreTax>();
                await _StoreTaxManager
                    .Create(mapData);

            }
            catch (Exception EX)
            {

            }
        }

        public async Task Update(UpdateStoreTaxInputDto input)
        {
            var mapData = input.MapTo<StoreTax>();
            await _StoreTaxManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreTaxInputDto input)
        {
            var mapData = input.MapTo<StoreTax>();
            await _StoreTaxManager
                .Delete(mapData);
        }


        public async Task<GetStoreTaxOutputDto> GetById(GetStoreTaxInputDto input)
        {
            var registration = await _StoreTaxManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreTaxOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreTaxOutputDto>> GetAdvanceSearch(StoreTaxAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreTaxManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreTaxOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetStoreTaxOutputDto>(mapDataquery);


            var filtereddatat = await _StoreTaxManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.TaxName.ToString().Contains(input.SearchText)
                || x.Rate.ToString().Contains(input.SearchText)
                || Convert.ToDateTime(x.LastModificationTime).ToString("MM-dd-yyyy hh:mm tt").Contains(input.SearchText)
                ).ToList();
            }

            if (input.StartDate != null && input.EndDate != null)
            {
                filtereddatat = filtereddatat.Where(x => x.LastModificationTime >= input.StartDate && x.LastModificationTime <= input.EndDate).ToList();
            }


            switch (input.SortDirection)
            {
                case "desc":
                    Console.WriteLine("This is part of outer switch ");

                    switch (input.SortColumn)
                    {
                        case "id":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "taxName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.TaxName).ToList();
                            break;
                        case "rate":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Rate).ToList();
                            break;

                        case "lastModificationTime":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.LastModificationTime).ToList();
                            break;
                        default:
                            filtereddatat = filtereddatat.OrderByDescending(x => x.LastModificationTime).ToList();
                            break;
                    }
                    break;
                default:
                    switch (input.SortColumn)
                    {
                        case "id":
                            filtereddatat = filtereddatat.OrderBy(x => x.Id).ToList();
                            break;
                        case "taxName":
                            filtereddatat = filtereddatat.OrderBy(x => x.TaxName).ToList();
                            break;
                        case "rate":
                            filtereddatat = filtereddatat.OrderBy(x => x.Rate).ToList();
                            break;
                        case "lastModificationTime":
                            filtereddatat = filtereddatat.OrderBy(x => x.LastModificationTime).ToList();
                            break;
                        default:
                            filtereddatat = filtereddatat.OrderBy(x => x.LastModificationTime).ToList();
                            break;
                    }
                    break;
            }

            //}

            filtereddatat = filtereddatat.Skip(input.CurrentPage).Take(input.MaxRecords).ToList();

            if (filtereddatat.Count() == 0)
            {
                return null;
            }
            else
            {
                var mapData = filtereddatat.MapTo<List<GetStoreTaxOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreTaxOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreTaxManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreTaxOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreTaxOutputDto));
                List<ExportStoreTaxOutputDto> csv = new List<ExportStoreTaxOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreTaxOutputDto temp = new ExportStoreTaxOutputDto();
                    temp.TaxName = regex.Replace(item.TaxName, m => m.ToString()).Replace(",", "");
                    temp.Rate = item.Rate;
                    temp.IsDefault = item.IsDefault;
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "TaxName,Rate,IsDefault,Date";
                engine.WriteFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ExportFiles/") + filename, csv);

                return ("/Download?filename=" + filename);
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }


        public async Task ImportFromCSV(string fileName)
        {
            try
            {
                //create a CSV engine using FileHelpers for your CSV file
                var engine = new FileHelperEngine(typeof(ExportStoreTaxOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreTaxOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreTaxInputDto> csv = new List<CreateStoreTaxInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreTaxInputDto temp = new CreateStoreTaxInputDto();
                        temp.Id = item.Id;
                        temp.TaxName = regex.Replace(item.TaxName, m => m.ToString()).Replace(",", "");
                        temp.Rate = item.Rate;
                        temp.IsDefault = item.IsDefault;
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreTaxManager
                         .Create(item.MapTo<StoreTax>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}