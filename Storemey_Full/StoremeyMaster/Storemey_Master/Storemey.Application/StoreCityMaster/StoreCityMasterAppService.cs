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
using Storemey.StoreCityMaster.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCityMaster
{
    [AbpAuthorize]
    public class StoreCityMasterAppService : AbpServiceBase, IStoreCityMasterAppService
    {
        private readonly IStoreCityMasterManager _StoreCityMasterManager;
        private readonly IRepository<StoreCityMaster, Guid> _StoreCityMasterRepository;

        public StoreCityMasterAppService(
            IStoreCityMasterManager StoreCityMasterManager,
            IRepository<StoreCityMaster, Guid> StoreCityMasterRepository)
        {
            _StoreCityMasterManager = StoreCityMasterManager;
            _StoreCityMasterRepository = StoreCityMasterRepository;
        }

        public async Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAll()
        {
            var events = await _StoreCityMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCityMasterOutputDto>>();
            return new ListResultDto<GetStoreCityMasterOutputDto>(returnData);
        }

        public async Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAllByStateId(long StateID)
        {
            var events = await _StoreCityMasterManager.ListAllByStateId(StateID);
            var returnData = events.MapTo<List<GetStoreCityMasterOutputDto>>();
            return new ListResultDto<GetStoreCityMasterOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCityMasterInputDto input)
        {
            var mapData = input.MapTo<StoreCityMaster>();
            await _StoreCityMasterManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreCityMasterInputDto input)
        {
            var mapData = input.MapTo<StoreCityMaster>();
            await _StoreCityMasterManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreCityMasterInputDto input)
        {
            var mapData = input.MapTo<StoreCityMaster>();
            await _StoreCityMasterManager
                .Delete(mapData);
        }


        public async Task<GetStoreCityMasterOutputDto> GetById(GetStoreCityMasterInputDto input)
        {
            var registration = await _StoreCityMasterManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCityMasterOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCityMasterOutputDto>> GetAdvanceSearch(StoreCityMasterAdvanceSearchInputDto input)
        {
            var filtereddatat = await _StoreCityMasterManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapData = filtereddatat.MapTo<List<GetStoreCityMasterOutputDto>>();
            //mapData.ForEach(x => x.recordsTotal = _StoreCityMasterManager.GetRecordCount(input.SearchText).Result);
            return new ListResultDto<GetStoreCityMasterOutputDto>(mapData);
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreCityMasterManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCityMasterOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreCityMasterOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreCityMasterOutputDto));
                List<ExportStoreCityMasterOutputDto> csv = new List<ExportStoreCityMasterOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreCityMasterOutputDto temp = new ExportStoreCityMasterOutputDto();
                    temp.CityName = regex.Replace(item.CityName, m => m.ToString()).Replace(",", "");
                    temp.Zipcode = regex.Replace(item.Zipcode, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreCityMasterOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreCityMasterOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreCityMasterInputDto> csv = new List<CreateStoreCityMasterInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreCityMasterInputDto temp = new CreateStoreCityMasterInputDto();
                        temp.Id = item.Id;
                        temp.CityName = regex.Replace(item.CityName, m => m.ToString()).Replace(",", "");
                        temp.Zipcode = regex.Replace(item.Zipcode, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreCityMasterManager
                         .Create(item.MapTo<StoreCityMaster>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}