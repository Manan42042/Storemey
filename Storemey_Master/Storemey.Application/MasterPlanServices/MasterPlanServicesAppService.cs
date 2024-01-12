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
using Storemey.MasterPlanServices.Dto;
using FileHelpers;
using System.Text.RegularExpressions;

namespace Storemey.MasterPlanServices
{
    [AbpAuthorize]
    public class MasterPlanServicesAppService :  IMasterPlanServicesAppService
    {
        private readonly IMasterPlanServicesManager _MasterPlanServicesManager;
        private readonly IRepository<MasterPlanServices, Guid> _MasterPlanServicesRepository;

        public MasterPlanServicesAppService(
            IMasterPlanServicesManager MasterPlanServicesManager, 
            IRepository<MasterPlanServices, Guid> MasterPlanServicesRepository)
        {
            _MasterPlanServicesManager = MasterPlanServicesManager;
            _MasterPlanServicesRepository = MasterPlanServicesRepository;
        }

        public async Task<ListResultDto<GetMasterPlanServicesOutputDto>> ListAll()
        {
            var events = await _MasterPlanServicesManager.ListAll();
            return new ListResultDto<GetMasterPlanServicesOutputDto>(events.MapTo<List<GetMasterPlanServicesOutputDto>>());
        }

        public ListResultDto<GetMasterPlanServicesOutputDto> GetByPlanId(int Id)
        {
            var events = _MasterPlanServicesManager.ListAll().Result.Where(x => x.PlanID == Id).ToList();
            return new ListResultDto<GetMasterPlanServicesOutputDto>(events.MapTo<List<GetMasterPlanServicesOutputDto>>());
        }
        public async Task Create(CreateMasterPlanServicesInputDto input)
        {
            var mapData = input.MapTo<MasterPlanServices>();
            await _MasterPlanServicesManager
                .Create(mapData);
        }

        public async Task Update(UpdateMasterPlanServicesInputDto input)
        {
            var mapData = input.MapTo<MasterPlanServices>();
            await _MasterPlanServicesManager
                .Update(mapData);
        }
        
        public async Task Delete(DeleteMasterPlanServicesInputDto input)
        {
            var mapData = input.MapTo<MasterPlanServices>();
            await _MasterPlanServicesManager
                .Delete(mapData);
        }
        
        
        public async Task<GetMasterPlanServicesOutputDto> GetById(GetMasterPlanServicesInputDto input)
        {
            var registration = await _MasterPlanServicesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetMasterPlanServicesOutputDto>();

            return mapData;
        }

        public async Task<ListResultDto<GetMasterPlanServicesOutputDto>> GetAdvanceSearch(MasterPlanServicesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _MasterPlanServicesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.sortColumn, input.sortDirection);

            var mapDataquery = filtereddatatquery.MapTo<List<GetMasterPlanServicesOutputDto>>();
            //mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetMasterPlanServicesOutputDto>(mapDataquery);


        }



        public async Task<string> ExportToCSV()
        {
            var events = await _MasterPlanServicesManager.ListAll();
            var returnData = events.MapTo<List<GetMasterPlanServicesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetMasterPlanServicesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportMasterPlanServicesOutputDto));
                List<ExportMasterPlanServicesOutputDto> csv = new List<ExportMasterPlanServicesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportMasterPlanServicesOutputDto temp = new ExportMasterPlanServicesOutputDto();
                    temp.ServiceName = item.ServiceName;
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "ServiceName,Date";
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
                var engine = new FileHelperEngine(typeof(ExportMasterPlanServicesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportMasterPlanServicesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<GetMasterPlanServicesOutputDto> csv = new List<GetMasterPlanServicesOutputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        GetMasterPlanServicesOutputDto temp = new GetMasterPlanServicesOutputDto();
                        temp.ServiceName = regex.Replace(item.ServiceName, m => m.ToString()).Replace(",", "");  //;
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _MasterPlanServicesManager
                         .Create(item.MapTo<MasterPlanServices>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}