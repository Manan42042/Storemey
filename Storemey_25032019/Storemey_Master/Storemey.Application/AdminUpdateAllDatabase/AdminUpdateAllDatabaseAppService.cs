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
using Storemey.AdminUpdateAllDatabase.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminUpdateAllDatabase
{
    [AbpAuthorize]
    public class AdminUpdateAllDatabaseAppService : AbpServiceBase, IAdminUpdateAllDatabaseAppService
    {
        private readonly IAdminUpdateAllDatabaseManager _AdminUpdateAllDatabaseManager;
        private readonly IRepository<AdminUpdateAllDatabase, Guid> _AdminUpdateAllDatabaseRepository;

        public AdminUpdateAllDatabaseAppService(
            IAdminUpdateAllDatabaseManager AdminUpdateAllDatabaseManager,
            IRepository<AdminUpdateAllDatabase, Guid> AdminUpdateAllDatabaseRepository)
        {
            _AdminUpdateAllDatabaseManager = AdminUpdateAllDatabaseManager;
            _AdminUpdateAllDatabaseRepository = AdminUpdateAllDatabaseRepository;
        }

        public async Task<ListResultDto<GetAdminUpdateAllDatabaseOutputDto>> ListAll()
        {
            var events = await _AdminUpdateAllDatabaseManager.ListAll();
            var returnData = events.MapTo<List<GetAdminUpdateAllDatabaseOutputDto>>();
            return new ListResultDto<GetAdminUpdateAllDatabaseOutputDto>(returnData);
        }


        public async Task Create(CreateAdminUpdateAllDatabaseInputDto input)
        {
            var mapData = input.MapTo<AdminUpdateAllDatabase>();
            await _AdminUpdateAllDatabaseManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminUpdateAllDatabaseInputDto input)
        {
            var mapData = input.MapTo<AdminUpdateAllDatabase>();
            await _AdminUpdateAllDatabaseManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminUpdateAllDatabaseInputDto input)
        {
            var mapData = input.MapTo<AdminUpdateAllDatabase>();
            await _AdminUpdateAllDatabaseManager
                .Delete(mapData);
        }


        public async Task<GetAdminUpdateAllDatabaseOutputDto> GetById(GetAdminUpdateAllDatabaseInputDto input)
        {
            var registration = await _AdminUpdateAllDatabaseManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminUpdateAllDatabaseOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminUpdateAllDatabaseOutputDto>> GetAdvanceSearch(AdminUpdateAllDatabaseAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminUpdateAllDatabaseManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminUpdateAllDatabaseOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _AdminUpdateAllDatabaseManager.GetRecordCount().Result);
            return new ListResultDto<GetAdminUpdateAllDatabaseOutputDto>(mapDataquery);


            var filtereddatat = await _AdminUpdateAllDatabaseManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Query.ToString().Contains(input.SearchText)
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
                        case "query":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Query).ToList();
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
                        case "query":
                            filtereddatat = filtereddatat.OrderBy(x => x.Query).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminUpdateAllDatabaseOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminUpdateAllDatabaseOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminUpdateAllDatabaseManager.ListAll();
            var returnData = events.MapTo<List<GetAdminUpdateAllDatabaseOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminUpdateAllDatabaseOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminUpdateAllDatabaseOutputDto));
                List<ExportAdminUpdateAllDatabaseOutputDto> csv = new List<ExportAdminUpdateAllDatabaseOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminUpdateAllDatabaseOutputDto temp = new ExportAdminUpdateAllDatabaseOutputDto();
                  
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
                var engine = new FileHelperEngine(typeof(ExportAdminUpdateAllDatabaseOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminUpdateAllDatabaseOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminUpdateAllDatabaseInputDto> csv = new List<CreateAdminUpdateAllDatabaseInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminUpdateAllDatabaseInputDto temp = new CreateAdminUpdateAllDatabaseInputDto();
                        temp.Id = item.Id;
                  
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _AdminUpdateAllDatabaseManager
                         .Create(item.MapTo<AdminUpdateAllDatabase>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}