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
using Storemey.StoreCountryMaster.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCountryMaster
{
    [AbpAuthorize]
    public class StoreCountryMasterAppService : AbpServiceBase, IStoreCountryMasterAppService
    {
        private readonly IStoreCountryMasterManager _StoreCountryMasterManager;
        private readonly IRepository<StoreCountryMaster, Guid> _StoreCountryMasterRepository;

        public StoreCountryMasterAppService(
            IStoreCountryMasterManager StoreCountryMasterManager,
            IRepository<StoreCountryMaster, Guid> StoreCountryMasterRepository)
        {
            _StoreCountryMasterManager = StoreCountryMasterManager;
            _StoreCountryMasterRepository = StoreCountryMasterRepository;
        }

        public async Task<ListResultDto<GetStoreCountryMasterOutputDto>> ListAll()
        {
            var events = await _StoreCountryMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCountryMasterOutputDto>>();
            return new ListResultDto<GetStoreCountryMasterOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCountryMasterInputDto input)
        {
            var mapData = input.MapTo<StoreCountryMaster>();
            await _StoreCountryMasterManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreCountryMasterInputDto input)
        {
            try
            {
                var mapData = input.MapTo<StoreCountryMaster>();
                await _StoreCountryMasterManager
                    .Update(mapData);
            }
            catch (Exception)
            {

            }
        }

        public async Task Delete(DeleteStoreCountryMasterInputDto input)
        {
            var mapData = input.MapTo<StoreCountryMaster>();
            await _StoreCountryMasterManager
                .Delete(mapData);
        }


        public async Task<GetStoreCountryMasterOutputDto> GetById(GetStoreCountryMasterInputDto input)
        {
            var registration = await _StoreCountryMasterManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCountryMasterOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCountryMasterOutputDto>> GetAdvanceSearch(StoreCountryMasterAdvanceSearchInputDto input)
        {
            var filtereddatatquery = await _StoreCountryMasterManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreCountryMasterOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreCountryMasterOutputDto>(mapDataquery);

            ////
            var filtereddatat = await _StoreCountryMasterManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.CountryName.ToString().Contains(input.SearchText)
                || x.CountryCode.ToString().Contains(input.SearchText)
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
                        case "countryName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.CountryName).ToList();
                            break;
                        case "countryCode":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.CountryCode).ToList();
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
                        case "countryName":
                            filtereddatat = filtereddatat.OrderBy(x => x.CountryName).ToList();
                            break;
                        case "countryCode":
                            filtereddatat = filtereddatat.OrderBy(x => x.CountryCode).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreCountryMasterOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreCountryMasterOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreCountryMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCountryMasterOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreCountryMasterOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreCountryMasterOutputDto));
                List<ExportStoreCountryMasterOutputDto> csv = new List<ExportStoreCountryMasterOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreCountryMasterOutputDto temp = new ExportStoreCountryMasterOutputDto();
                    temp.CountryName = regex.Replace(item.CountryName, m => m.ToString()).Replace(",", "");
                    temp.CountryCode = regex.Replace(item.CountryCode, m => m.ToString()).Replace(",", "");
                    temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreCountryMasterOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreCountryMasterOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreCountryMasterInputDto> csv = new List<CreateStoreCountryMasterInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreCountryMasterInputDto temp = new CreateStoreCountryMasterInputDto();
                        temp.Id = item.Id;
                        temp.CountryName = regex.Replace(item.CountryName, m => m.ToString()).Replace(",", "");
                        temp.CountryCode = regex.Replace(item.CountryCode, m => m.ToString()).Replace(",", "");
                        temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreCountryMasterManager
                         .Create(item.MapTo<StoreCountryMaster>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}