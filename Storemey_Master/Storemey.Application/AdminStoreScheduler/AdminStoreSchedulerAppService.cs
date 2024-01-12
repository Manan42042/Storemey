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
using Storemey.AdminStoreScheduler.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminStoreScheduler
{
    [AbpAuthorize]
    public class AdminStoreSchedulerAppService : AbpServiceBase, IAdminStoreSchedulerAppService
    {
        private readonly IAdminStoreSchedulerManager _AdminStoreSchedulerManager;
        private readonly IRepository<AdminStoreScheduler, Guid> _AdminStoreSchedulerRepository;

        public AdminStoreSchedulerAppService(
            IAdminStoreSchedulerManager AdminStoreSchedulerManager,
            IRepository<AdminStoreScheduler, Guid> AdminStoreSchedulerRepository)
        {
            _AdminStoreSchedulerManager = AdminStoreSchedulerManager;
            _AdminStoreSchedulerRepository = AdminStoreSchedulerRepository;
        }

        public async Task<ListResultDto<GetAdminStoreSchedulerOutputDto>> ListAll()
        {
            var events = await _AdminStoreSchedulerManager.ListAll();
            var returnData = events.MapTo<List<GetAdminStoreSchedulerOutputDto>>();
            return new ListResultDto<GetAdminStoreSchedulerOutputDto>(returnData);
        }


        public async Task Create(CreateAdminStoreSchedulerInputDto input)
        {
            var mapData = input.MapTo<AdminStoreScheduler>();
            await _AdminStoreSchedulerManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminStoreSchedulerInputDto input)
        {
            var mapData = input.MapTo<AdminStoreScheduler>();
            await _AdminStoreSchedulerManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminStoreSchedulerInputDto input)
        {
            var mapData = input.MapTo<AdminStoreScheduler>();
            await _AdminStoreSchedulerManager
                .Delete(mapData);
        }


        public async Task<GetAdminStoreSchedulerOutputDto> GetById(GetAdminStoreSchedulerInputDto input)
        {
            var registration = await _AdminStoreSchedulerManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminStoreSchedulerOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminStoreSchedulerOutputDto>> GetAdvanceSearch(AdminStoreSchedulerAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _AdminStoreSchedulerManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminStoreSchedulerOutputDto>>();
            return new ListResultDto<GetAdminStoreSchedulerOutputDto>(mapDataquery);


            var filtereddatat = await _AdminStoreSchedulerManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.FromEmail.ToString().Contains(input.SearchText)
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
                        case "fromEmail":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.FromEmail).ToList();
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
                        case "fromEmail":
                            filtereddatat = filtereddatat.OrderBy(x => x.FromEmail).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminStoreSchedulerOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminStoreSchedulerOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminStoreSchedulerManager.ListAll();
            var returnData = events.MapTo<List<GetAdminStoreSchedulerOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminStoreSchedulerOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminStoreSchedulerOutputDto));
                List<ExportAdminStoreSchedulerOutputDto> csv = new List<ExportAdminStoreSchedulerOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminStoreSchedulerOutputDto temp = new ExportAdminStoreSchedulerOutputDto();
                  
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
                var engine = new FileHelperEngine(typeof(ExportAdminStoreSchedulerOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminStoreSchedulerOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminStoreSchedulerInputDto> csv = new List<CreateAdminStoreSchedulerInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminStoreSchedulerInputDto temp = new CreateAdminStoreSchedulerInputDto();
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
                    await _AdminStoreSchedulerManager
                         .Create(item.MapTo<AdminStoreScheduler>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}