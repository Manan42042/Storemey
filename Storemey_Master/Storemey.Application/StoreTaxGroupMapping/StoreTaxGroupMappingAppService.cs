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
using Storemey.StoreTaxGroupLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreTaxGroupLinks
{
    [AbpAuthorize]
    public class StoreTaxGroupLinksAppService : AbpServiceBase, IStoreTaxGroupLinksAppService
    {
        private readonly IStoreTaxGroupLinksManager _StoreTaxGroupLinksManager;
        private readonly IRepository<StoreTaxGroupLinks, Guid> _StoreTaxGroupLinksRepository;

        public StoreTaxGroupLinksAppService(
            IStoreTaxGroupLinksManager StoreTaxGroupLinksManager,
            IRepository<StoreTaxGroupLinks, Guid> StoreTaxGroupLinksRepository)
        {
            _StoreTaxGroupLinksManager = StoreTaxGroupLinksManager;
            _StoreTaxGroupLinksRepository = StoreTaxGroupLinksRepository;
        }

        public async Task<ListResultDto<GetStoreTaxGroupLinksOutputDto>> ListAll()
        {
            var events = await _StoreTaxGroupLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxGroupLinksOutputDto>>();
            return new ListResultDto<GetStoreTaxGroupLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreTaxGroupLinksInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroupLinks>();
            await _StoreTaxGroupLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreTaxGroupLinksInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroupLinks>();
            await _StoreTaxGroupLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreTaxGroupLinksInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroupLinks>();
            await _StoreTaxGroupLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreTaxGroupLinksOutputDto> GetById(GetStoreTaxGroupLinksInputDto input)
        {
            var registration = await _StoreTaxGroupLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreTaxGroupLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreTaxGroupLinksOutputDto>> GetAdvanceSearch(StoreTaxGroupLinksAdvanceSearchInputDto input)
        {
            var filtereddatat = await _StoreTaxGroupLinksManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.TaxId.ToString().Contains(input.SearchText)
                || x.TaxGroupId.ToString().Contains(input.SearchText)
                
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
                        case "taxId":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.TaxId).ToList();
                            break;
                        case "taxGroupId":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.TaxGroupId).ToList();
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
                        case "taxId":
                            filtereddatat = filtereddatat.OrderBy(x => x.TaxId).ToList();
                            break;
                        case "taxGroupId":
                            filtereddatat = filtereddatat.OrderBy(x => x.TaxGroupId).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreTaxGroupLinksOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreTaxGroupLinksOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreTaxGroupLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxGroupLinksOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreTaxGroupLinksOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreTaxGroupLinksOutputDto));
                List<ExportStoreTaxGroupLinksOutputDto> csv = new List<ExportStoreTaxGroupLinksOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreTaxGroupLinksOutputDto temp = new ExportStoreTaxGroupLinksOutputDto();
                    temp.TaxId = item.TaxId;
                    temp.TaxGroupId = item.TaxGroupId;
                    temp.Note = item.Note;
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "TaxId,TaxGroupId,Note,Date";
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
                var engine = new FileHelperEngine(typeof(ExportStoreTaxGroupLinksOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreTaxGroupLinksOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreTaxGroupLinksInputDto> csv = new List<CreateStoreTaxGroupLinksInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreTaxGroupLinksInputDto temp = new CreateStoreTaxGroupLinksInputDto();
                        temp.Id = item.Id;
                        temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
                        temp.TaxId = item.TaxId;
                        temp.TaxGroupId = item.TaxGroupId;
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreTaxGroupLinksManager
                         .Create(item.MapTo<StoreTaxGroupLinks>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}