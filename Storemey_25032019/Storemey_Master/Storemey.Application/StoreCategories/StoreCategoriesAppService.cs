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
using Storemey.StoreCategories.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.Application;
using System.Web.Hosting;

namespace Storemey.StoreCategories
{
    [AbpAuthorize]
    public class StoreCategoriesAppService : AbpServiceBase, IStoreCategoriesAppService
    {
        private readonly IStoreCategoriesManager _StoreCategoriesManager;
        private readonly IRepository<StoreCategories, Guid> _StoreCategoriesRepository;
        string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";

        public StoreCategoriesAppService(
            IStoreCategoriesManager StoreCategoriesManager,
            IRepository<StoreCategories, Guid> StoreCategoriesRepository)
        {
            _StoreCategoriesManager = StoreCategoriesManager;
            _StoreCategoriesRepository = StoreCategoriesRepository;
        }

        public async Task<ListResultDto<GetStoreCategoriesOutputDto>> ListAll()
        {
            var events = await _StoreCategoriesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCategoriesOutputDto>>();
            return new ListResultDto<GetStoreCategoriesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreCategoriesInputDto input)
        {
            try
            {
                var mapData = input.MapTo<StoreCategories>();
                await _StoreCategoriesManager
                    .Create(mapData);
                if (!string.IsNullOrEmpty(input.Image) && input.Image.Contains("base64"))
                {
                    CommonEntityHelper.Savenewimage(input.Image, HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + CatergoryImage + backslace, mapData.Id.ToString(), "");
                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task Update(UpdateStoreCategoriesInputDto input)
        {
            try
            {
                var mapData = input.MapTo<StoreCategories>();
                await _StoreCategoriesManager
                    .Update(mapData);
                if (!string.IsNullOrEmpty(input.Image) && input.Image.Contains("base64"))
                {
                    CommonEntityHelper.Savenewimage(input.Image, HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + CatergoryImage + backslace, mapData.Id.ToString(), mapData.Id.ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async Task Delete(DeleteStoreCategoriesInputDto input)
        {
            var mapData = input.MapTo<StoreCategories>();
            await _StoreCategoriesManager
                .Delete(mapData);
        }


        public async Task<GetStoreCategoriesOutputDto> GetById(GetStoreCategoriesInputDto input)
        {
            var registration = await _StoreCategoriesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreCategoriesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreCategoriesOutputDto>> GetAdvanceSearch(StoreCategoriesAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreCategoriesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreCategoriesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreCategoriesManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreCategoriesOutputDto>(mapDataquery);
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreCategoriesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreCategoriesOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreCategoriesOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreCategoriesOutputDto));
                List<ExportStoreCategoriesOutputDto> csv = new List<ExportStoreCategoriesOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    ExportStoreCategoriesOutputDto temp = new ExportStoreCategoriesOutputDto();
                    temp.CategoryName = regex.Replace(item.CategoryName, m => m.ToString()).Replace(",", "");
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
                var engine = new FileHelperEngine(typeof(ExportStoreCategoriesOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreCategoriesOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreCategoriesInputDto> csv = new List<CreateStoreCategoriesInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreCategoriesInputDto temp = new CreateStoreCategoriesInputDto();
                        temp.Id = item.Id;
                        temp.CategoryName = regex.Replace(item.CategoryName, m => m.ToString()).Replace(",", "");
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
                    await _StoreCategoriesManager
                         .Create(item.MapTo<StoreCategories>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}