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
using Storemey.StoreTags.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreTags
{
    [AbpAuthorize]
    public class StoreTagsAppService : AbpServiceBase, IStoreTagsAppService
    {
        private readonly IStoreTagsManager _StoreTagsManager;
        private readonly IRepository<StoreTags, Guid> _StoreTagsRepository;

        public StoreTagsAppService(
            IStoreTagsManager StoreTagsManager,
            IRepository<StoreTags, Guid> StoreTagsRepository)
        {
            _StoreTagsManager = StoreTagsManager;
            _StoreTagsRepository = StoreTagsRepository;
        }

        public async Task<ListResultDto<GetStoreTagsOutputDto>> ListAll()
        {
            var events = await _StoreTagsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTagsOutputDto>>();
            return new ListResultDto<GetStoreTagsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreTagsInputDto input)
        {
            var mapData = input.MapTo<StoreTags>();
            await _StoreTagsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreTagsInputDto input)
        {
            var mapData = input.MapTo<StoreTags>();
            await _StoreTagsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreTagsInputDto input)
        {
            var mapData = input.MapTo<StoreTags>();
            await _StoreTagsManager
                .Delete(mapData);
        }


        public async Task<GetStoreTagsOutputDto> GetById(GetStoreTagsInputDto input)
        {
            var registration = await _StoreTagsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreTagsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreTagsOutputDto>> GetAdvanceSearch(StoreTagsAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreTagsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreTagsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreTagsManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreTagsOutputDto>(mapDataquery);


            var filtereddatat = await _StoreTagsManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.TagName.ToString().Contains(input.SearchText)
                || x.Description.ToString().Contains(input.SearchText)
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
                        case "tagName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.TagName).ToList();
                            break;
                        case "description":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Description).ToList();
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
                        case "tagName":
                            filtereddatat = filtereddatat.OrderBy(x => x.TagName).ToList();
                            break;
                        case "description":
                            filtereddatat = filtereddatat.OrderBy(x => x.Description).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreTagsOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreTagsOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreTagsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTagsOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreTagsOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreTagsOutputDto));
                List<ExportStoreTagsOutputDto> csv = new List<ExportStoreTagsOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreTagsOutputDto temp = new ExportStoreTagsOutputDto();
                    temp.TagName = regex.Replace(item.TagName, m => m.ToString()).Replace(",", "");
                    temp.Description = regex.Replace(item.Description, m => m.ToString()).Replace(",", "");
                    temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreTagsOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreTagsOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreTagsInputDto> csv = new List<CreateStoreTagsInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreTagsInputDto temp = new CreateStoreTagsInputDto();
                        temp.Id = item.Id;
                        temp.TagName = regex.Replace(item.TagName, m => m.ToString()).Replace(",", "");
                        temp.Description = regex.Replace(item.Description, m => m.ToString()).Replace(",", "");
                        temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");

                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreTagsManager
                         .Create(item.MapTo<StoreTags>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}