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
using Storemey.AdminStores.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminStores
{
    [AbpAuthorize]
    public class AdminStoresAppService : AbpServiceBase, IAdminStoresAppService
    {
        private readonly IAdminStoresManager _AdminStoresManager;
        private readonly IRepository<AdminStores, Guid> _AdminStoresRepository;
       
        public AdminStoresAppService(
            IAdminStoresManager AdminStoresManager,
            IRepository<AdminStores, Guid> AdminStoresRepository)
        {
            _AdminStoresManager = AdminStoresManager;
            _AdminStoresRepository = AdminStoresRepository;


        }
        ~AdminStoresAppService()
        {
      
        }

        public async Task<ListResultDto<GetAdminStoresOutputDto>> ListAll()
        {

            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;

            var events = await _AdminStoresManager.ListAll();
            var returnData = events.MapTo<List<GetAdminStoresOutputDto>>();

            StoremeyConsts.tenantName = oldvalue;

            return new ListResultDto<GetAdminStoresOutputDto>(returnData);
        }


        public async Task Create(CreateAdminStoresInputDto input)
        {
            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;

            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Create(mapData);
            StoremeyConsts.tenantName = oldvalue;
        }

        public async Task Update(UpdateAdminStoresInputDto input)
        {
            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;

            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Update(mapData);

            StoremeyConsts.tenantName = oldvalue;

            StoremeyConsts.isFirstTimeLogin = false;
        }

        public async Task Delete(DeleteAdminStoresInputDto input)
        {
            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Delete(mapData);
        }


        public async Task<GetAdminStoresOutputDto> GetById(GetAdminStoresInputDto input)
        {

            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;
            var registration = await _AdminStoresManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminStoresOutputDto>();
            StoremeyConsts.tenantName = oldvalue;

            return mapData;
        }


        public async Task<GetAdminStoresOutputDto> GetByStoreName(string storename)
        {

            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;

            var registration = await _AdminStoresManager.GetByStoreName(storename);
            var mapData = registration.MapTo<GetAdminStoresOutputDto>();

            StoremeyConsts.tenantName = oldvalue;
            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminStoresOutputDto>> GetAdvanceSearch(AdminStoresAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminStoresManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminStoresOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _AdminStoresManager.GetRecordCount().Result);
            return new ListResultDto<GetAdminStoresOutputDto>(mapDataquery);


            var filtereddatat = await _AdminStoresManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.FirstName.ToString().Contains(input.SearchText)
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
                        case "firstName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.FirstName).ToList();
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
                        case "firstName":
                            filtereddatat = filtereddatat.OrderBy(x => x.FirstName).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminStoresOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminStoresOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminStoresManager.ListAll();
            var returnData = events.MapTo<List<GetAdminStoresOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminStoresOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminStoresOutputDto));
                List<ExportAdminStoresOutputDto> csv = new List<ExportAdminStoresOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminStoresOutputDto temp = new ExportAdminStoresOutputDto();
                  
                    temp.Name = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
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
                var engine = new FileHelperEngine(typeof(ExportAdminStoresOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminStoresOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminStoresInputDto> csv = new List<CreateAdminStoresInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminStoresInputDto temp = new CreateAdminStoresInputDto();
                        temp.Id = item.Id;
                    
                        temp.LastModificationTime = DateTime.UtcNow;
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _AdminStoresManager
                         .Create(item.MapTo<AdminStores>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}