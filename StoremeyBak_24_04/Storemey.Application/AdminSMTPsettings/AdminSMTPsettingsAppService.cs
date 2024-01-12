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
using Storemey.AdminSMTPsettings.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminSMTPsettings
{
    [AbpAuthorize]
    public class AdminSMTPsettingsAppService : AbpServiceBase, IAdminSMTPsettingsAppService
    {
        private readonly IAdminSMTPsettingsManager _AdminSMTPsettingsManager;
        private readonly IRepository<AdminSMTPsettings, Guid> _AdminSMTPsettingsRepository;

        public AdminSMTPsettingsAppService(
            IAdminSMTPsettingsManager AdminSMTPsettingsManager,
            IRepository<AdminSMTPsettings, Guid> AdminSMTPsettingsRepository)
        {
            _AdminSMTPsettingsManager = AdminSMTPsettingsManager;
            _AdminSMTPsettingsRepository = AdminSMTPsettingsRepository;
        }

        public async Task<ListResultDto<GetAdminSMTPsettingsOutputDto>> ListAll()
        {
            var events = await _AdminSMTPsettingsManager.ListAll();
            var returnData = events.MapTo<List<GetAdminSMTPsettingsOutputDto>>();
            return new ListResultDto<GetAdminSMTPsettingsOutputDto>(returnData);
        }


        public async Task Create(CreateAdminSMTPsettingsInputDto input)
        {
            var mapData = input.MapTo<AdminSMTPsettings>();
            await _AdminSMTPsettingsManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminSMTPsettingsInputDto input)
        {
            var mapData = input.MapTo<AdminSMTPsettings>();
            await _AdminSMTPsettingsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminSMTPsettingsInputDto input)
        {
            var mapData = input.MapTo<AdminSMTPsettings>();
            await _AdminSMTPsettingsManager
                .Delete(mapData);
        }


        public async Task<GetAdminSMTPsettingsOutputDto> GetById(GetAdminSMTPsettingsInputDto input)
        {
            var registration = await _AdminSMTPsettingsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminSMTPsettingsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminSMTPsettingsOutputDto>> GetAdvanceSearch(AdminSMTPsettingsAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminSMTPsettingsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminSMTPsettingsOutputDto>>();
            return new ListResultDto<GetAdminSMTPsettingsOutputDto>(mapDataquery);


            var filtereddatat = await _AdminSMTPsettingsManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Host.ToString().Contains(input.SearchText)
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
                        case "host":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Host).ToList();
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
                        case "host":
                            filtereddatat = filtereddatat.OrderBy(x => x.Host).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminSMTPsettingsOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminSMTPsettingsOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminSMTPsettingsManager.ListAll();
            var returnData = events.MapTo<List<GetAdminSMTPsettingsOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminSMTPsettingsOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminSMTPsettingsOutputDto));
                List<ExportAdminSMTPsettingsOutputDto> csv = new List<ExportAdminSMTPsettingsOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminSMTPsettingsOutputDto temp = new ExportAdminSMTPsettingsOutputDto();
                
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
                var engine = new FileHelperEngine(typeof(ExportAdminSMTPsettingsOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminSMTPsettingsOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminSMTPsettingsInputDto> csv = new List<CreateAdminSMTPsettingsInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminSMTPsettingsInputDto temp = new CreateAdminSMTPsettingsInputDto();
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
                    await _AdminSMTPsettingsManager
                         .Create(item.MapTo<AdminSMTPsettings>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}