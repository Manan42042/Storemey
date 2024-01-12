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
using Storemey.MasterPlans.Dto;
using Storemey.MasterPlanServices;
using Storemey.MasterPlanServices.Dto;
using Storemey.MasterPlanPrices;
using Storemey.MasterPlanPrices.Dto;
using FileHelpers;
using System.Text.RegularExpressions;

namespace Storemey.MasterPlans
{
    [AbpAuthorize]
    public class MasterPlansAppService :  IMasterPlansAppService
    {
        private readonly IMasterPlansManager _MasterPlansManager;
        private readonly IMasterPlanServicesManager _MasterPlanServicesManager;
        private readonly IRepository<MasterPlans, Guid> _MasterPlansRepository;
        private readonly IMasterPlanPricesManager _MasterPlanPricesManager;

        public MasterPlansAppService(
            IMasterPlansManager MasterPlansManager,
            IMasterPlanServicesManager MasterPlanServicesManager,
            IRepository<MasterPlans, Guid> MasterPlansRepository,
            IMasterPlanPricesManager MasterPlanPricesManager)
        {
            _MasterPlansManager = MasterPlansManager;
            _MasterPlansRepository = MasterPlansRepository;
            _MasterPlanServicesManager = MasterPlanServicesManager;
            _MasterPlanPricesManager = MasterPlanPricesManager;
        }

        public async Task<ListResultDto<GetMasterPlansOutputDto>> ListAll()
        {
            var events = await _MasterPlansManager.ListAll();
            var mapData = new ListResultDto<GetMasterPlansOutputDto>(events.MapTo<List<GetMasterPlansOutputDto>>());

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<List<GetMasterPlansOutputDto>> GetPlanAndServices()
        {
            var events = await _MasterPlansManager.ListAll();
            var mapData = events.MapTo<List<GetMasterPlansOutputDto>>();

            foreach (var item in mapData)
            {
                item.PlanServices = _MasterPlanServicesManager.GetByPlanID(item.Id).MapTo<List<GetMasterPlanServicesOutputDto>>();
                item.Price = _MasterPlanPricesManager.GetByPlanIDAndCountryID(230, item.Id).MapTo<GetMasterPlanPricesOutputDto>();
            }

            return mapData;
        }

        public async Task Create(CreateMasterPlansInputDto input)
        {
            var mapData = input.MapTo<MasterPlans>();
            await _MasterPlansManager
                .Create(mapData);
        }

        public async Task Update(UpdateMasterPlansInputDto input)
        {
            var mapData = input.MapTo<MasterPlans>();
            await _MasterPlansManager
                .Update(mapData);
        }
        
        public async Task Delete(DeleteMasterPlansInputDto input)
        {
            var mapData = input.MapTo<MasterPlans>();
            await _MasterPlansManager
                .Delete(mapData);
        }
        
        
        public async Task<GetMasterPlansOutputDto> GetById(GetMasterPlansInputDto input)
        {
            var registration = await _MasterPlansManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetMasterPlansOutputDto>();

            return mapData;
        }

        public async Task<ListResultDto<GetMasterPlansOutputDto>> GetAdvanceSearch(MasterPlansAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _MasterPlansManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.sortColumn, input.sortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetMasterPlansOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _MasterPlansManager.GetRecordCount().Result);
            return new ListResultDto<GetMasterPlansOutputDto>(mapDataquery);


            var filtereddatat = await _MasterPlansManager.ListAll();


            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.PlanName.ToString().Contains(input.SearchText)
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
                        case "PlanName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.PlanName).ToList();
                            break;
                        case "lastModificationTime":
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
                        case "PlanName":
                            filtereddatat = filtereddatat.OrderBy(x => x.PlanName).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetMasterPlansOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetMasterPlansOutputDto>(mapData);
            }
        }



        public async Task<string> ExportToCSV()
        {
            var events = await _MasterPlansManager.ListAll();
            var returnData = events.MapTo<List<GetMasterPlansOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetMasterPlansOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportMasterPlansOutputDto));
                List<ExportMasterPlansOutputDto> csv = new List<ExportMasterPlansOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportMasterPlansOutputDto temp = new ExportMasterPlansOutputDto();
                    temp.PlanName = item.PlanName;
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "PlanName,Date";
                engine.WriteFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ExportFiles/") + filename, csv);

                return ("/Download?filename=" + filename);
            }
            catch (Exception ex)
            {
            }
            return string.Empty;
        }


        public async Task ImportFromCSV(string filename)
        {
            try
            {
                //create a CSV engine using FileHelpers for your CSV file
                var engine = new FileHelperEngine(typeof(ExportMasterPlansOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportMasterPlansOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + filename);
                List<GetMasterPlansOutputDto> csv = new List<GetMasterPlansOutputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        GetMasterPlansOutputDto temp = new GetMasterPlansOutputDto();
                        temp.PlanName = regex.Replace(item.PlanName, m => m.ToString()).Replace(",", "");  //;
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _MasterPlansManager
                         .Create(item.MapTo<MasterPlans>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}