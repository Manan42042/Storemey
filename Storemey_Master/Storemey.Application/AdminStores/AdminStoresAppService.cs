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
        private readonly StoreUsers.StoreUsersManager _storeUsersManager;
        public AdminStoresAppService(
            IAdminStoresManager AdminStoresManager,
            IRepository<AdminStores, Guid> AdminStoresRepository, StoreUsers.StoreUsersManager storeUsersManager)
        {
            _AdminStoresManager = AdminStoresManager;
            _AdminStoresRepository = AdminStoresRepository;
            _storeUsersManager = storeUsersManager;


        }
        ~AdminStoresAppService()
        {

        }

        public async Task<ListResultDto<GetAdminStoresOutputDto>> ListAll()
        {

            //string oldvalue = StoremeyConsts.tenantName;
            //StoremeyConsts.tenantName = string.Empty;


            StoremeyConsts.isMaindatabse = true;
            var events = await _AdminStoresManager.ListAll();
            var returnData = events.MapTo<List<GetAdminStoresOutputDto>>();
            StoremeyConsts.isMaindatabse = false;


            //StoremeyConsts.tenantName = oldvalue;

            return new ListResultDto<GetAdminStoresOutputDto>(returnData);
        }


        public async Task Create(CreateAdminStoresInputDto input)
        {
            //string oldvalue = StoremeyConsts.tenantName;
            //StoremeyConsts.tenantName = string.Empty;



            StoremeyConsts.isMaindatabse = true;
            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Create(mapData);
            StoremeyConsts.isMaindatabse = false;


            //StoremeyConsts.tenantName = oldvalue;

        }

        public async Task Update(UpdateAdminStoresInputDto input)
        {
            StoremeyConsts.isMaindatabse = true;

            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Update(mapData);
            StoremeyConsts.isMaindatabse = false;
            StoremeyConsts.isFirstTimeLogin = false;
        }



        public async Task UpdateUsersTables (UpdateAdminStoresInputDto input)
        {
            StoreUsers.StoreUsers storeUsers = await _storeUsersManager.GetByABPID(1);
            storeUsers.FirstName = input.FirstName;
            storeUsers.LastName = input.LastName;
            storeUsers.Image = StoremeyConsts.defaultImage;
            storeUsers.EmailAddress = StoremeyConsts.tenanEmail;
            storeUsers.PhoneNumber = input.Mobile;
            storeUsers.UserName = StoremeyConsts.tenantUserName;
            storeUsers.Password = StoremeyConsts.tenantPassword;
            //storeUsers.NormalPassword = StoremeyConsts.tenantPassword;
            storeUsers.ABPUserId = 1;
            await _storeUsersManager.Update(storeUsers);
        }

        public async Task Delete(DeleteAdminStoresInputDto input)
        {
            var mapData = input.MapTo<AdminStores>();
            await _AdminStoresManager
                .Delete(mapData);
        }


        public async Task<GetAdminStoresOutputDto> GetById(GetAdminStoresInputDto input)
        {

            //string oldvalue = StoremeyConsts.tenantName;
            //StoremeyConsts.tenantName = string.Empty;



            StoremeyConsts.isMaindatabse = true;
            var registration = await _AdminStoresManager.GetByID(input.Id);
            StoremeyConsts.isMaindatabse = false;


            var mapData = registration.MapTo<GetAdminStoresOutputDto>();
            //StoremeyConsts.tenantName = oldvalue;

            return mapData;
        }


        public async Task<GetAdminStoresOutputDto> GetByStoreName(string storename)
        {

            //string oldvalue = StoremeyConsts.tenantName;
            //StoremeyConsts.tenantName = string.Empty;

            var registration = await _AdminStoresManager.GetByStoreName(storename);
            var mapData = registration.MapTo<GetAdminStoresOutputDto>();

            //StoremeyConsts.tenantName = oldvalue;
            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminStoresOutputDto>> GetAdvanceSearch(AdminStoresAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminStoresManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);
            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetAdminStoresOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetAdminStoresOutputDto>(mapDataquery);


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