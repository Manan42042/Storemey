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
using Storemey.StorePaymentTypes.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StorePaymentTypes
{
    [AbpAuthorize]
    public class StorePaymentTypesAppService : AbpServiceBase, IStorePaymentTypesAppService
    {
        private readonly IStorePaymentTypesManager _StorePaymentTypesManager;
        private readonly IRepository<StorePaymentTypes, Guid> _StorePaymentTypesRepository;

        public StorePaymentTypesAppService(
            IStorePaymentTypesManager StorePaymentTypesManager,
            IRepository<StorePaymentTypes, Guid> StorePaymentTypesRepository)
        {
            _StorePaymentTypesManager = StorePaymentTypesManager;
            _StorePaymentTypesRepository = StorePaymentTypesRepository;
        }

        public async Task<ListResultDto<GetStorePaymentTypesOutputDto>> ListAll()
        {
            var events = await _StorePaymentTypesManager.ListAll();
            var returnData = events.MapTo<List<GetStorePaymentTypesOutputDto>>();
            return new ListResultDto<GetStorePaymentTypesOutputDto>(returnData);
        }


        public async Task Create(CreateStorePaymentTypesInputDto input)
        {
            var mapData = input.MapTo<StorePaymentTypes>();
            await _StorePaymentTypesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStorePaymentTypesInputDto input)
        {
            var mapData = input.MapTo<StorePaymentTypes>();
            await _StorePaymentTypesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStorePaymentTypesInputDto input)
        {
            var mapData = input.MapTo<StorePaymentTypes>();
            await _StorePaymentTypesManager
                .Delete(mapData);
        }


        public async Task<GetStorePaymentTypesOutputDto> GetById(GetStorePaymentTypesInputDto input)
        {
            var registration = await _StorePaymentTypesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStorePaymentTypesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStorePaymentTypesOutputDto>> GetAdvanceSearch(StorePaymentTypesAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StorePaymentTypesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStorePaymentTypesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StorePaymentTypesManager.GetRecordCount().Result);
            return new ListResultDto<GetStorePaymentTypesOutputDto>(mapDataquery);


            var filtereddatat = await _StorePaymentTypesManager.ListAll();
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
                var mapData = filtereddatat.MapTo<List<GetStorePaymentTypesOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStorePaymentTypesOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StorePaymentTypesManager.ListAll();
            var returnData = events.MapTo<List<GetStorePaymentTypesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStorePaymentTypesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStorePaymentTypesOutputDto));
                List<ExportStorePaymentTypesOutputDto> csv = new List<ExportStorePaymentTypesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStorePaymentTypesOutputDto temp = new ExportStorePaymentTypesOutputDto();
                    temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
                    temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "Name,Note,Date";
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
                var engine = new FileHelperEngine(typeof(ExportStorePaymentTypesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStorePaymentTypesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStorePaymentTypesInputDto> csv = new List<CreateStorePaymentTypesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStorePaymentTypesInputDto temp = new CreateStorePaymentTypesInputDto();
                        temp.Id = item.Id;
                        temp.Name = regex.Replace(item.Name, m => m.ToString()).Replace(",", "");
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
                    await _StorePaymentTypesManager
                         .Create(item.MapTo<StorePaymentTypes>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}