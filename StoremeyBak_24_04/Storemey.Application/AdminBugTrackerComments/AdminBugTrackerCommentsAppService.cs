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
using Storemey.AdminBugTrackerComments.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminBugTrackerComments
{
    [AbpAuthorize]
    public class AdminBugTrackerCommentsAppService : AbpServiceBase, IAdminBugTrackerCommentsAppService
    {
        private readonly IAdminBugTrackerCommentsManager _AdminBugTrackerCommentsManager;
        private readonly IRepository<AdminBugTrackerComments, Guid> _AdminBugTrackerCommentsRepository;

        public AdminBugTrackerCommentsAppService(
            IAdminBugTrackerCommentsManager AdminBugTrackerCommentsManager,
            IRepository<AdminBugTrackerComments, Guid> AdminBugTrackerCommentsRepository)
        {
            _AdminBugTrackerCommentsManager = AdminBugTrackerCommentsManager;
            _AdminBugTrackerCommentsRepository = AdminBugTrackerCommentsRepository;
        }

        public async Task<ListResultDto<GetAdminBugTrackerCommentsOutputDto>> ListAll()
        {
            var events = await _AdminBugTrackerCommentsManager.ListAll();
            var returnData = events.MapTo<List<GetAdminBugTrackerCommentsOutputDto>>();
            return new ListResultDto<GetAdminBugTrackerCommentsOutputDto>(returnData);
        }


        public async Task Create(CreateAdminBugTrackerCommentsInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackerComments>();
            await _AdminBugTrackerCommentsManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminBugTrackerCommentsInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackerComments>();
            await _AdminBugTrackerCommentsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminBugTrackerCommentsInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackerComments>();
            await _AdminBugTrackerCommentsManager
                .Delete(mapData);
        }


        public async Task<GetAdminBugTrackerCommentsOutputDto> GetById(GetAdminBugTrackerCommentsInputDto input)
        {
            var registration = await _AdminBugTrackerCommentsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminBugTrackerCommentsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminBugTrackerCommentsOutputDto>> GetAdvanceSearch(AdminBugTrackerCommentsAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminBugTrackerCommentsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminBugTrackerCommentsOutputDto>>();
            return new ListResultDto<GetAdminBugTrackerCommentsOutputDto>(mapDataquery);


            var filtereddatat = await _AdminBugTrackerCommentsManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Comment.ToString().Contains(input.SearchText)
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
                        case "comment":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Comment).ToList();
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
                        case "comment":
                            filtereddatat = filtereddatat.OrderBy(x => x.Comment).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetAdminBugTrackerCommentsOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetAdminBugTrackerCommentsOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminBugTrackerCommentsManager.ListAll();
            var returnData = events.MapTo<List<GetAdminBugTrackerCommentsOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminBugTrackerCommentsOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminBugTrackerCommentsOutputDto));
                List<ExportAdminBugTrackerCommentsOutputDto> csv = new List<ExportAdminBugTrackerCommentsOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminBugTrackerCommentsOutputDto temp = new ExportAdminBugTrackerCommentsOutputDto();
              
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
                var engine = new FileHelperEngine(typeof(ExportAdminBugTrackerCommentsOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminBugTrackerCommentsOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminBugTrackerCommentsInputDto> csv = new List<CreateAdminBugTrackerCommentsInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminBugTrackerCommentsInputDto temp = new CreateAdminBugTrackerCommentsInputDto();
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
                    await _AdminBugTrackerCommentsManager
                         .Create(item.MapTo<AdminBugTrackerComments>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}