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
using Storemey.StoreReceiptTemplates.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreReceiptTemplates
{
    [AbpAuthorize]
    public class StoreReceiptTemplatesAppService : AbpServiceBase, IStoreReceiptTemplatesAppService
    {
        private readonly IStoreReceiptTemplatesManager _StoreReceiptTemplatesManager;
        private readonly IRepository<StoreReceiptTemplates, Guid> _StoreReceiptTemplatesRepository;

        public StoreReceiptTemplatesAppService(
            IStoreReceiptTemplatesManager StoreReceiptTemplatesManager,
            IRepository<StoreReceiptTemplates, Guid> StoreReceiptTemplatesRepository)
        {
            _StoreReceiptTemplatesManager = StoreReceiptTemplatesManager;
            _StoreReceiptTemplatesRepository = StoreReceiptTemplatesRepository;
        }

        public async Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> ListAll()
        {
            var events = await _StoreReceiptTemplatesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreReceiptTemplatesOutputDto>>();
            return new ListResultDto<GetStoreReceiptTemplatesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreReceiptTemplatesInputDto input)
        {
            var mapData = input.MapTo<StoreReceiptTemplates>();
            await _StoreReceiptTemplatesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreReceiptTemplatesInputDto input)
        {
            var mapData = input.MapTo<StoreReceiptTemplates>();
            await _StoreReceiptTemplatesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreReceiptTemplatesInputDto input)
        {
            var mapData = input.MapTo<StoreReceiptTemplates>();
            await _StoreReceiptTemplatesManager
                .Delete(mapData);
        }


        public async Task<GetStoreReceiptTemplatesOutputDto> GetById(GetStoreReceiptTemplatesInputDto input)
        {
            var registration = await _StoreReceiptTemplatesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreReceiptTemplatesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> GetAdvanceSearch(StoreReceiptTemplatesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreReceiptTemplatesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreReceiptTemplatesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreReceiptTemplatesManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreReceiptTemplatesOutputDto>(mapDataquery);


            var filtereddatat = await _StoreReceiptTemplatesManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.Name.ToString().Contains(input.SearchText)
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
                        case "name":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Name).ToList();
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
                        case "name":
                            filtereddatat = filtereddatat.OrderBy(x => x.Name).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreReceiptTemplatesOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreReceiptTemplatesOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreReceiptTemplatesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreReceiptTemplatesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreReceiptTemplatesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreReceiptTemplatesOutputDto));
                List<ExportStoreReceiptTemplatesOutputDto> csv = new List<ExportStoreReceiptTemplatesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreReceiptTemplatesOutputDto temp = new ExportStoreReceiptTemplatesOutputDto();
                    temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
                    temp.HeaderText = regex.Replace(item.HeaderText, m => m.ToString()).Replace(",", "");
                    temp.InvoiceNoPrefix = regex.Replace(item.InvoiceNoPrefix, m => m.ToString()).Replace(",", "");
                    temp.InoviceHeading = regex.Replace(item.InoviceHeading, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreReceiptTemplatesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreReceiptTemplatesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreReceiptTemplatesInputDto> csv = new List<CreateStoreReceiptTemplatesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreReceiptTemplatesInputDto temp = new CreateStoreReceiptTemplatesInputDto();
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
                    await _StoreReceiptTemplatesManager
                         .Create(item.MapTo<StoreReceiptTemplates>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}