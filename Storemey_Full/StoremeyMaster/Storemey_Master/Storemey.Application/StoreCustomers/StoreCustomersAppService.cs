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
using Storemey.StoreCustomers.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreCustomers
{
    [AbpAuthorize]
    public class StoreCustomersAppService : AbpServiceBase, IStoreCustomersAppService
    {
        private readonly IStoreCustomersManager _StoreCustomersManager;
        private readonly IRepository<StoreCustomers, Guid> _StoreCustomersRepository;

        public StoreCustomersAppService(
            IStoreCustomersManager StoreCustomersManager,
            IRepository<StoreCustomers, Guid> StoreCustomersRepository)
        {
            _StoreCustomersManager = StoreCustomersManager;
            _StoreCustomersRepository = StoreCustomersRepository;
        }

        public async Task<ListResultDto<GetStoreCustomersOutputDto>> ListAll()
        {
            var events = await _StoreCustomersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCustomersOutputDto>>();
            returnData.ForEach(x => x.CustomerName = x.FirstName + " " + x.LastName);
            return new ListResultDto<GetStoreCustomersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCustomersInputDto input)
        {
            try
            {

            var mapData = input.MapTo<StoreCustomers>();
            await _StoreCustomersManager
                .Create(mapData);

            }
            catch (Exception)
            {

            }
        }

        public async Task Update(UpdateStoreCustomersInputDto input)
        {
            var mapData = input.MapTo<StoreCustomers>();
            await _StoreCustomersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreCustomersInputDto input)
        {
            var mapData = input.MapTo<StoreCustomers>();
            await _StoreCustomersManager
                .Delete(mapData);
        }


        public async Task<GetStoreCustomersOutputDto> GetById(GetStoreCustomersInputDto input)
        {
            var registration = await _StoreCustomersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCustomersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCustomersOutputDto>> GetAdvanceSearch(StoreCustomersAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreCustomersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreCustomersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreCustomersOutputDto>(mapDataquery);



            var filtereddatat = await _StoreCustomersManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.CustomerCode.ToString().Contains(input.SearchText)
                || x.FirstName.ToString().Contains(input.SearchText)
                || x.LastName.ToString().Contains(input.SearchText)
                || x.Gender.ToString().Contains(input.SearchText)
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
                        case "customerCode":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.CustomerCode).ToList();
                            break;
                        case "firstName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case "gender":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Gender).ToList();
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
                        case "customerCode":
                            filtereddatat = filtereddatat.OrderBy(x => x.CustomerCode).ToList();
                            break;
                        case "firstName":
                            filtereddatat = filtereddatat.OrderBy(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderBy(x => x.LastName).ToList();
                            break;
                        case "gender":
                            filtereddatat = filtereddatat.OrderBy(x => x.Gender).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreCustomersOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreCustomersOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreCustomersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCustomersOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreCustomersOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreCustomersOutputDto));
                List<ExportStoreCustomersOutputDto> csv = new List<ExportStoreCustomersOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreCustomersOutputDto temp = new ExportStoreCustomersOutputDto();
                    temp.CustomerCode = regex.Replace(item.CustomerCode, m => m.ToString()).Replace(",", "");
                    temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                    temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
                    temp.Gender = regex.Replace(item.Gender, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreCustomersOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreCustomersOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreCustomersInputDto> csv = new List<CreateStoreCustomersInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreCustomersInputDto temp = new CreateStoreCustomersInputDto();
                        temp.Id = item.Id;
                        temp.CustomerCode = regex.Replace(item.CustomerCode, m => m.ToString()).Replace(",", "");
                        temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                        temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
                        temp.Gender = regex.Replace(item.Gender, m => m.ToString()).Replace(",", "");

                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreCustomersManager
                         .Create(item.MapTo<StoreCustomers>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}