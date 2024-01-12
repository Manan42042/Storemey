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
using Storemey.StoreSuppliers.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreSuppliers
{
    [AbpAuthorize]
    public class StoreSuppliersAppService : AbpServiceBase, IStoreSuppliersAppService
    {
        private readonly IStoreSuppliersManager _StoreSuppliersManager;
        private readonly IRepository<StoreSuppliers, Guid> _StoreSuppliersRepository;

        public StoreSuppliersAppService(
            IStoreSuppliersManager StoreSuppliersManager,
            IRepository<StoreSuppliers, Guid> StoreSuppliersRepository)
        {
            _StoreSuppliersManager = StoreSuppliersManager;
            _StoreSuppliersRepository = StoreSuppliersRepository;
        }

        public async Task<ListResultDto<GetStoreSuppliersOutputDto>> ListAll()
        {
            var events = await _StoreSuppliersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSuppliersOutputDto>>();
            return new ListResultDto<GetStoreSuppliersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreSuppliersInputDto input)
        {
            try
            {

            var mapData = input.MapTo<StoreSuppliers>();
            await _StoreSuppliersManager
                .Create(mapData);

            }
            catch (Exception EX)
            {

            }
        }

        public async Task Update(UpdateStoreSuppliersInputDto input)
        {
            var mapData = input.MapTo<StoreSuppliers>();
            await _StoreSuppliersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreSuppliersInputDto input)
        {
            var mapData = input.MapTo<StoreSuppliers>();
            await _StoreSuppliersManager
                .Delete(mapData);
        }


        public async Task<GetStoreSuppliersOutputDto> GetById(GetStoreSuppliersInputDto input)
        {
            var registration = await _StoreSuppliersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreSuppliersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreSuppliersOutputDto>> GetAdvanceSearch(StoreSuppliersAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreSuppliersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreSuppliersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreSuppliersOutputDto>(mapDataquery);

            var filtereddatat = await _StoreSuppliersManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.SupplierFullName.ToString().Contains(input.SearchText)
                || x.Description.ToString().Contains(input.SearchText)
                || x.FirstName.ToString().Contains(input.SearchText)
                || x.LastName.ToString().Contains(input.SearchText)
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
                        case "supplierFullName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.SupplierFullName).ToList();
                            break;
                        case "description":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Description).ToList();
                            break;
                        case "firstName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.LastName).ToList();
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
                        case "supplierFullName":
                            filtereddatat = filtereddatat.OrderBy(x => x.SupplierFullName).ToList();
                            break;
                        case "description":
                            filtereddatat = filtereddatat.OrderBy(x => x.Description).ToList();
                            break;
                        case "firstName":
                            filtereddatat = filtereddatat.OrderBy(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderBy(x => x.LastName).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreSuppliersOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreSuppliersOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreSuppliersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSuppliersOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreSuppliersOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreSuppliersOutputDto));
                List<ExportStoreSuppliersOutputDto> csv = new List<ExportStoreSuppliersOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreSuppliersOutputDto temp = new ExportStoreSuppliersOutputDto();
                    temp.SupplierFullName = regex.Replace(item.SupplierFullName, m => m.ToString()).Replace(",", "");
                    temp.Description = regex.Replace(item.Description, m => m.ToString()).Replace(",", "");
                    temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                    temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreSuppliersOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreSuppliersOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreSuppliersInputDto> csv = new List<CreateStoreSuppliersInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreSuppliersInputDto temp = new CreateStoreSuppliersInputDto();
                        temp.Id = item.Id;
                        temp.SupplierFullName = regex.Replace(item.SupplierFullName, m => m.ToString()).Replace(",", "");
                        temp.Description = regex.Replace(item.Description, m => m.ToString()).Replace(",", "");
                        temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                        temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
                        temp.LastModificationTime = Convert.ToDateTime(item.Date);
                        temp.DeleterUserId = 1;
                        temp.DeletionTime = DateTime.UtcNow;
                        temp.LastModifierUserId = 1;
                        csv.Add(temp);
                    }
                }
                foreach (var item in csv)
                {
                    await _StoreSuppliersManager
                         .Create(item.MapTo<StoreSuppliers>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}