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
using Storemey.MasterCountries.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.MasterCountries
{
    [AbpAuthorize]
    public class MasterCountriesAppService : AbpServiceBase, IMasterCountriesAppService
    {
        private readonly IMasterCountriesManager _masterCountriesManager;
        private readonly IRepository<MasterCountries, Guid> _masterCountriesRepository;

        public MasterCountriesAppService(
            IMasterCountriesManager masterCountriesManager,
            IRepository<MasterCountries, Guid> masterCountriesRepository)
        {
            _masterCountriesManager = masterCountriesManager;
            _masterCountriesRepository = masterCountriesRepository;
        }

        public async Task<ListResultDto<GetMasterCountriesOutputDto>> ListAll()
        {
            var events = await _masterCountriesManager.ListAll();
            var returnData = events.MapTo<List<GetMasterCountriesOutputDto>>();
            return new ListResultDto<GetMasterCountriesOutputDto>(returnData);
        }


        public async Task Create(CreateMasterCountriesInputDto input)
        {
            var mapData = input.MapTo<MasterCountries>();
            await _masterCountriesManager
                .Create(mapData);
        }

        public async Task Update(UpdateMasterCountriesInputDto input)
        {
            var mapData = input.MapTo<MasterCountries>();
            await _masterCountriesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteMasterCountriesInputDto input)
        {
            var mapData = input.MapTo<MasterCountries>();
            await _masterCountriesManager
                .Delete(mapData);
        }


        public async Task<GetMasterCountriesOutputDto> GetById(GetMasterCountriesInputDto input)
        {
            var registration = await _masterCountriesManager.GetByID(input.Id);

            if (registration == null)
            {
                return new GetMasterCountriesOutputDto();
            }

            var mapData = registration.MapTo<GetMasterCountriesOutputDto>();

            return mapData;
        }

        public async Task<ListResultDto<GetMasterCountriesOutputDto>> GetAdvanceSearch(MasterCountriesAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _masterCountriesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.sortColumn, input.sortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetMasterCountriesOutputDto>>();
            return new ListResultDto<GetMasterCountriesOutputDto>(mapDataquery);


            var filtereddatat = await _masterCountriesManager.ListAll();
            int maxCount = filtereddatat == null ? 0 : filtereddatat.Count();

            if (filtereddatat == null)
            {
                return null;
            }

                if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Name.ToString().Contains(input.SearchText)
                || x.Currency_Name.ToString().Contains(input.SearchText)
                || x.Current_Code.ToString().Contains(input.SearchText)
                || Convert.ToDateTime(x.LastModificationTime).ToString("MM-dd-yyyy hh:mm tt").Contains(input.SearchText)
                ).ToList();
            }

            if (input.StartDate != null && input.EndDate != null)
            {
                filtereddatat = filtereddatat.Where(x => x.LastModificationTime >= input.StartDate && x.LastModificationTime <= input.EndDate).ToList();
            }


            switch (input.sortDirection)
            {
                case "desc":
                    Console.WriteLine("This is part of outer switch ");

                    switch (input.sortColumn)
                    {
                        case "id":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Id).ToList();
                            break;
                        case "name":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Name).ToList();
                            break;
                        case "currency_Name":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Currency_Name).ToList();
                            break;
                        case "current_Code":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Current_Code).ToList();
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
                    switch (input.sortColumn)
                    {
                        case "id":
                            filtereddatat = filtereddatat.OrderBy(x => x.Id).ToList();
                            break;
                        case "name":
                            filtereddatat = filtereddatat.OrderBy(x => x.Name).ToList();
                            break;
                        case "currency_Name":
                            filtereddatat = filtereddatat.OrderBy(x => x.Currency_Name).ToList();
                            break;
                        case "current_Code":
                            filtereddatat = filtereddatat.OrderBy(x => x.Current_Code).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetMasterCountriesOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetMasterCountriesOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _masterCountriesManager.ListAll();
            var returnData = events.MapTo<List<GetMasterCountriesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetMasterCountriesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportMasterCountriesOutputDto));
                List<ExportMasterCountriesOutputDto> csv = new List<ExportMasterCountriesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportMasterCountriesOutputDto temp = new ExportMasterCountriesOutputDto();
                    temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
                    temp.Code = regex.Replace(item.Code, m => m.ToString()).Replace(",", "");
                    temp.Dail_Code = regex.Replace(item.Dail_Code, m => m.ToString()).Replace(",", "");
                    temp.Currency_Name = regex.Replace(item.Currency_Name, m => m.ToString()).Replace(",", "");
                    temp.Curreny_Symbol = regex.Replace(item.Curreny_Symbol, m => m.ToString()).Replace(",", "");
                    temp.Current_Code = regex.Replace(item.Current_Code, m => m.ToString()).Replace(",", "");
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "Name,Code,Dail_Code,Currency_Name,Curreny_Symbol,Current_Code,Date";
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
                var engine = new FileHelperEngine(typeof(ExportMasterCountriesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportMasterCountriesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateMasterCountriesInputDto> csv = new List<CreateMasterCountriesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateMasterCountriesInputDto temp = new CreateMasterCountriesInputDto();
                        temp.Id = item.Id;
                        temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
                        temp.Code = regex.Replace(item.Code, m => m.ToString()).Replace(",", "");
                        temp.Dail_Code = regex.Replace(item.Dail_Code, m => m.ToString()).Replace(",", "");
                        temp.Currency_Name = regex.Replace(item.Currency_Name, m => m.ToString()).Replace(",", "");
                        temp.Curreny_Symbol = regex.Replace(item.Curreny_Symbol, m => m.ToString()).Replace(",", "");
                        temp.Current_Code = regex.Replace(item.Current_Code, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = DateTime.UtcNow;
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _masterCountriesManager
                         .Create(item.MapTo<MasterCountries>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}