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
using Storemey.AdminBugTrackers.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.AdminBugTrackers
{
    [AbpAuthorize]
    public class AdminBugTrackersAppService : AbpServiceBase, IAdminBugTrackersAppService
    {
        private readonly IAdminBugTrackersManager _AdminBugTrackersManager;
        private readonly IRepository<AdminBugTrackers, Guid> _AdminBugTrackersRepository;

        public AdminBugTrackersAppService(
            IAdminBugTrackersManager AdminBugTrackersManager,
            IRepository<AdminBugTrackers, Guid> AdminBugTrackersRepository)
        {
            _AdminBugTrackersManager = AdminBugTrackersManager;
            _AdminBugTrackersRepository = AdminBugTrackersRepository;
        }

        public async Task<ListResultDto<GetAdminBugTrackersOutputDto>> ListAll()
        {
            var events = await _AdminBugTrackersManager.ListAll();
            var returnData = events.MapTo<List<GetAdminBugTrackersOutputDto>>();
            return new ListResultDto<GetAdminBugTrackersOutputDto>(returnData);
        }


        public async Task Create(CreateAdminBugTrackersInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackers>();
            await _AdminBugTrackersManager
                .Create(mapData);
        }

        public async Task Update(UpdateAdminBugTrackersInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackers>();
            await _AdminBugTrackersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteAdminBugTrackersInputDto input)
        {
            var mapData = input.MapTo<AdminBugTrackers>();
            await _AdminBugTrackersManager
                .Delete(mapData);
        }


        public async Task<GetAdminBugTrackersOutputDto> GetById(GetAdminBugTrackersInputDto input)
        {
            var registration = await _AdminBugTrackersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetAdminBugTrackersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetAdminBugTrackersOutputDto>> GetAdvanceSearch(AdminBugTrackersAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _AdminBugTrackersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetAdminBugTrackersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _AdminBugTrackersManager.GetRecordCount().Result);
            return new ListResultDto<GetAdminBugTrackersOutputDto>(mapDataquery);


            var filtereddatat = await _AdminBugTrackersManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.BugFromName.ToString().Contains(input.SearchText)
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
                        case "bugFromName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.BugFromName).ToList();
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
                        case "bugFromName":
                            filtereddatat = filtereddatat.OrderBy(x => x.BugFromName).ToList();
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
                try
                {

                    var mapData = filtereddatat.MapTo<List<GetAdminBugTrackersOutputDto>>();
                    mapData.ForEach(x => x.recordsTotal = maxCount);
                    return new ListResultDto<GetAdminBugTrackersOutputDto>(mapData);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _AdminBugTrackersManager.ListAll();
            var returnData = events.MapTo<List<GetAdminBugTrackersOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetAdminBugTrackersOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportAdminBugTrackersOutputDto));
                List<ExportAdminBugTrackersOutputDto> csv = new List<ExportAdminBugTrackersOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportAdminBugTrackersOutputDto temp = new ExportAdminBugTrackersOutputDto();
               
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
                var engine = new FileHelperEngine(typeof(ExportAdminBugTrackersOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportAdminBugTrackersOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateAdminBugTrackersInputDto> csv = new List<CreateAdminBugTrackersInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateAdminBugTrackersInputDto temp = new CreateAdminBugTrackersInputDto();
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
                    await _AdminBugTrackersManager
                         .Create(item.MapTo<AdminBugTrackers>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}