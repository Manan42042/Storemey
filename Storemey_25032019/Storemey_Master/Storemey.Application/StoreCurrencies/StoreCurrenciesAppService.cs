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
using Storemey.StoreCurrencies.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCurrencies
{
    [AbpAuthorize]
    public class StoreCurrenciesAppService : AbpServiceBase, IStoreCurrenciesAppService
    {
        private readonly IStoreCurrenciesManager _StoreCurrenciesManager;
        private readonly IRepository<StoreCurrencies, Guid> _StoreCurrenciesRepository;

        public StoreCurrenciesAppService(
            IStoreCurrenciesManager StoreCurrenciesManager,
            IRepository<StoreCurrencies, Guid> StoreCurrenciesRepository)
        {
            _StoreCurrenciesManager = StoreCurrenciesManager;
            _StoreCurrenciesRepository = StoreCurrenciesRepository;
        }

        public async Task<ListResultDto<GetStoreCurrenciesOutputDto>> ListAll()
        {
            var events = await _StoreCurrenciesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCurrenciesOutputDto>>();
            returnData.ForEach(x => x.Currency = x.Name + " / " + x.Currency + " (" + x.Symbol + ") ");

            return new ListResultDto<GetStoreCurrenciesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCurrenciesInputDto input)
        {
            var mapData = input.MapTo<StoreCurrencies>();
            await _StoreCurrenciesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreCurrenciesInputDto input)
        {
            var mapData = input.MapTo<StoreCurrencies>();
            await _StoreCurrenciesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreCurrenciesInputDto input)
        {
            var mapData = input.MapTo<StoreCurrencies>();
            await _StoreCurrenciesManager
                .Delete(mapData);
        }


        public async Task<GetStoreCurrenciesOutputDto> GetById(GetStoreCurrenciesInputDto input)
        {
            var registration = await _StoreCurrenciesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCurrenciesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCurrenciesOutputDto>> GetAdvanceSearch(StoreCurrenciesAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreCurrenciesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreCurrenciesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreCurrenciesManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreCurrenciesOutputDto>(mapDataquery);


            var filtereddatat = await _StoreCurrenciesManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Name.ToString().Contains(input.SearchText)
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
                var mapData = filtereddatat.MapTo<List<GetStoreCurrenciesOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreCurrenciesOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreCurrenciesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCurrenciesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreCurrenciesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreCurrenciesOutputDto));
                List<ExportStoreCurrenciesOutputDto> csv = new List<ExportStoreCurrenciesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreCurrenciesOutputDto temp = new ExportStoreCurrenciesOutputDto();
                    temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "Currancy_Name,Currency_Symbol,Currency_Code,Currency_Code02,Date";
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
                var engine = new FileHelperEngine(typeof(ExportStoreCurrenciesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreCurrenciesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreCurrenciesInputDto> csv = new List<CreateStoreCurrenciesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreCurrenciesInputDto temp = new CreateStoreCurrenciesInputDto();
                        temp.Id = item.Id;
                        temp.Country = regex.Replace(item.Country, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreCurrenciesManager
                         .Create(item.MapTo<StoreCurrencies>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}