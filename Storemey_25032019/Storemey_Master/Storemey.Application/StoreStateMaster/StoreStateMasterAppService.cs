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
using Storemey.StoreStateMaster.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreStateMaster
{
    [AbpAuthorize]
    public class StoreStateMasterAppService : AbpServiceBase, IStoreStateMasterAppService
    {
        private readonly IStoreStateMasterManager _StoreStateMasterManager;
        private readonly IRepository<StoreStateMaster, Guid> _StoreStateMasterRepository;

        public StoreStateMasterAppService(
            IStoreStateMasterManager StoreStateMasterManager,
            IRepository<StoreStateMaster, Guid> StoreStateMasterRepository)
        {
            _StoreStateMasterManager = StoreStateMasterManager;
            _StoreStateMasterRepository = StoreStateMasterRepository;
        }

        public async Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAll()
        {
            var events = await _StoreStateMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreStateMasterOutputDto>>();
            return new ListResultDto<GetStoreStateMasterOutputDto>(returnData);
        }

        public async Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAllByCountryID(long Id)
        {
            var events = await _StoreStateMasterManager.ListAllByCountryID(Id);
            var returnData = events.MapTo<List<GetStoreStateMasterOutputDto>>();
            return new ListResultDto<GetStoreStateMasterOutputDto>(returnData);
        }


        public async Task Create(CreateStoreStateMasterInputDto input)
        {
            var mapData = input.MapTo<StoreStateMaster>();
            await _StoreStateMasterManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreStateMasterInputDto input)
        {
            var mapData = input.MapTo<StoreStateMaster>();
            await _StoreStateMasterManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreStateMasterInputDto input)
        {
            var mapData = input.MapTo<StoreStateMaster>();
            await _StoreStateMasterManager
                .Delete(mapData);
        }


        public async Task<GetStoreStateMasterOutputDto> GetById(GetStoreStateMasterInputDto input)
        {
            var registration = await _StoreStateMasterManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreStateMasterOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreStateMasterOutputDto>> GetAdvanceSearch(StoreStateMasterAdvanceSearchInputDto input)
        {

            try
            {


                var filtereddatatquery = await _StoreStateMasterManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.MapTo<List<GetStoreStateMasterOutputDto>>();
                mapDataquery.ForEach(x => x.recordsTotal = _StoreStateMasterManager.GetRecordCount().Result);
                return new ListResultDto<GetStoreStateMasterOutputDto>(mapDataquery);

            }
            catch (Exception ex)
            {

            }
            return null;

            var filtereddatat = await _StoreStateMasterManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.CountryId.ToString().Contains(input.SearchText)
                || x.StateName.ToString().Contains(input.SearchText)
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
                        case "countryId":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.CountryId).ToList();
                            break;
                        case "stateName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.StateName).ToList();
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
                        case "countryId":
                            filtereddatat = filtereddatat.OrderBy(x => x.CountryId).ToList();
                            break;
                        case "stateName":
                            filtereddatat = filtereddatat.OrderBy(x => x.StateName).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreStateMasterOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreStateMasterOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreStateMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreStateMasterOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreStateMasterOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreStateMasterOutputDto));
                List<ExportStoreStateMasterOutputDto> csv = new List<ExportStoreStateMasterOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreStateMasterOutputDto temp = new ExportStoreStateMasterOutputDto();
                    temp.StateName = regex.Replace(item.StateName, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreStateMasterOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreStateMasterOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreStateMasterInputDto> csv = new List<CreateStoreStateMasterInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreStateMasterInputDto temp = new CreateStoreStateMasterInputDto();
                        temp.Id = item.Id;
                        temp.StateName = regex.Replace(item.StateName, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreStateMasterManager
                         .Create(item.MapTo<StoreStateMaster>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}