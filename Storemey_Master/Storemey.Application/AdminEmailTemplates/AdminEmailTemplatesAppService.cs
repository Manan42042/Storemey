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
using Storemey.AdminEmailTemplates.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminEmailTemplates
{
    [AbpAuthorize]
    public class AdminEmailTemplatesAppService : AbpServiceBase, IAdminEmailTemplatesAppService
    {
        private readonly IAdminEmailTemplatesManager _AdminEmailTemplatesManager;
        private readonly IRepository<AdminEmailTemplates, Guid> _AdminEmailTemplatesRepository;

        public AdminEmailTemplatesAppService(
            IAdminEmailTemplatesManager AdminEmailTemplatesManager,
            IRepository<AdminEmailTemplates, Guid> AdminEmailTemplatesRepository)
        {
            _AdminEmailTemplatesManager = AdminEmailTemplatesManager;
            _AdminEmailTemplatesRepository = AdminEmailTemplatesRepository;
        }

        public async Task<ListResultDto<GetAdminEmailTemplatesOutputDto>> ListAll()
        {
            var events = await _AdminEmailTemplatesManager.ListAll();
            var returnData = events.MapTo<List<GetAdminEmailTemplatesOutputDto>>();
            return new ListResultDto<GetAdminEmailTemplatesOutputDto>(returnData);
        }


        public async Task Create(CreateAdminEmailTemplatesInputDto input)
        {
            var mapData = input.MapTo<AdminEmailTemplates>();
            await _AdminEmailTemplatesManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminEmailTemplatesInputDto input)
        {
            var mapData = input.MapTo<AdminEmailTemplates>();
            await _AdminEmailTemplatesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminEmailTemplatesInputDto input)
        {
            var mapData = input.MapTo<AdminEmailTemplates>();
            await _AdminEmailTemplatesManager
                .Delete(mapData);
        }


        public async Task<GetAdminEmailTemplatesOutputDto> GetById(GetAdminEmailTemplatesInputDto input)
        {
            var registration = await _AdminEmailTemplatesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminEmailTemplatesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminEmailTemplatesOutputDto>> GetAdvanceSearch(AdminEmailTemplatesAdvanceSearchInputDto input)
        {
            var filtereddatatquery = await _AdminEmailTemplatesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminEmailTemplatesOutputDto>>();
            return new ListResultDto<GetAdminEmailTemplatesOutputDto>(mapDataquery);


            var filtereddatat = await _AdminEmailTemplatesManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.EmailKey.ToString().Contains(input.SearchText)
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
                        case "emailKey":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.EmailKey).ToList();
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
                        case "emailKey":
                            filtereddatat = filtereddatat.OrderBy(x => x.EmailKey).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminEmailTemplatesOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminEmailTemplatesOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminEmailTemplatesManager.ListAll();
            var returnData = events.MapTo<List<GetAdminEmailTemplatesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminEmailTemplatesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminEmailTemplatesOutputDto));
                List<ExportAdminEmailTemplatesOutputDto> csv = new List<ExportAdminEmailTemplatesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminEmailTemplatesOutputDto temp = new ExportAdminEmailTemplatesOutputDto();
               
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
                var engine = new FileHelperEngine(typeof(ExportAdminEmailTemplatesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminEmailTemplatesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminEmailTemplatesInputDto> csv = new List<CreateAdminEmailTemplatesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminEmailTemplatesInputDto temp = new CreateAdminEmailTemplatesInputDto();
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
                    await _AdminEmailTemplatesManager
                         .Create(item.MapTo<AdminEmailTemplates>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}