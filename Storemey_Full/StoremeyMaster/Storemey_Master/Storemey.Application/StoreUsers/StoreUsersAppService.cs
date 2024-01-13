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
using Storemey.StoreUsers.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.Application;
using System.Web.Hosting;

namespace Storemey.StoreUsers
{
    [AbpAuthorize]
    public class StoreUsersAppService : AbpServiceBase, IStoreUsersAppService
    {
        private readonly IStoreUsersManager _StoreUsersManager;
        private readonly IRepository<StoreUsers, Guid> _StoreUsersRepository;
        string folderImage = "images", folderupload = "upload", folderexport = "export", folderbackup = "backup", folderROOT = "storeDocuments", backslace = "/", productImage = "productImage", userImages = "userImages", CatergoryImage = "CatergoryImage", storeImage = "storeImage";

        public StoreUsersAppService(
            IStoreUsersManager StoreUsersManager,
            IRepository<StoreUsers, Guid> StoreUsersRepository)
        {
            _StoreUsersManager = StoreUsersManager;
            _StoreUsersRepository = StoreUsersRepository;
        }

        public async Task<ListResultDto<GetStoreUsersOutputDto>> ListAll()
        {
            var events = await _StoreUsersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreUsersOutputDto>>();
            return new ListResultDto<GetStoreUsersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreUsersInputDto input)
        {
            var mapData = input.MapTo<StoreUsers>();
            await _StoreUsersManager
                .Create(mapData);

            if (!string.IsNullOrEmpty(input.Image) && input.Image.Contains("base64"))
            {
                CommonEntityHelper.Savenewimage(input.Image,  HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + userImages + backslace, mapData.Id.ToString(), "");
            }
        }

        public async Task Update(UpdateStoreUsersInputDto input)
        {
            var mapData = input.MapTo<StoreUsers>();
            await _StoreUsersManager
                .Update(mapData);

            if (!string.IsNullOrEmpty(input.Image) && input.Image.Contains("base64"))
            {
                CommonEntityHelper.Savenewimage(input.Image,  HostingEnvironment.ApplicationPhysicalPath + backslace + folderROOT + backslace + StoremeyConsts.StoreName + backslace + folderImage + backslace + userImages + backslace, mapData.Id.ToString(), mapData.Id.ToString());
            }
        }

        public async Task Delete(DeleteStoreUsersInputDto input)
        {
            var mapData = input.MapTo<StoreUsers>();
            await _StoreUsersManager
                .Delete(mapData);
        }


        public async Task<GetStoreUsersOutputDto> GetById(GetStoreUsersInputDto input)
        {
            var registration = await _StoreUsersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreUsersOutputDto>();

            return mapData;
        }




        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreUsersOutputDto>> GetAdvanceSearch(StoreUsersAdvanceSearchInputDto input)
        {

            var filtereddatatquery = await _StoreUsersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreUsersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetStoreUsersOutputDto>(mapDataquery);


            var filtereddatat = await _StoreUsersManager.ListAll();
            int maxCount = filtereddatat.Count();

            if (!string.IsNullOrEmpty(input.SearchText))
            {
                filtereddatat = filtereddatat.Where(x => x.Id.ToString().Contains(input.SearchText)
                || x.FirstName.ToString().Contains(input.SearchText)
                || x.LastName.ToString().Contains(input.SearchText)
                || x.EmailAddress.ToString().Contains(input.SearchText)
                || x.PhoneNumber.ToString().Contains(input.SearchText)
                || x.UserName.ToString().Contains(input.SearchText)
                || x.Note.ToString().Contains(input.SearchText)
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
                        case "firstName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case "emailAddress":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.EmailAddress).ToList();
                            break;
                        case "phoneNumber":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.PhoneNumber).ToList();
                            break;
                        case "userName":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.UserName).ToList();
                            break;
                        case "note":
                            filtereddatat = filtereddatat.OrderByDescending(x => x.Note).ToList();
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
                        case "firstName":
                            filtereddatat = filtereddatat.OrderBy(x => x.FirstName).ToList();
                            break;
                        case "lastName":
                            filtereddatat = filtereddatat.OrderBy(x => x.LastName).ToList();
                            break;
                        case "emailAddress":
                            filtereddatat = filtereddatat.OrderBy(x => x.EmailAddress).ToList();
                            break;
                        case "phoneNumber":
                            filtereddatat = filtereddatat.OrderBy(x => x.PhoneNumber).ToList();
                            break;
                        case "userName":
                            filtereddatat = filtereddatat.OrderBy(x => x.UserName).ToList();
                            break;
                        case "note":
                            filtereddatat = filtereddatat.OrderBy(x => x.Note).ToList();
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
                var mapData = filtereddatat.MapTo<List<GetStoreUsersOutputDto>>();
                mapData.ForEach(x => x.recordsTotal = maxCount);
                return new ListResultDto<GetStoreUsersOutputDto>(mapData);
            }
        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreUsersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreUsersOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreUsersOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreUsersOutputDto));
                List<ExportStoreUsersOutputDto> csv = new List<ExportStoreUsersOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    try
                    {
                        ExportStoreUsersOutputDto temp = new ExportStoreUsersOutputDto();
                        temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                        temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
                        temp.EmailAddress = regex.Replace(item.EmailAddress, m => m.ToString()).Replace(",", "");
                        temp.PhoneNumber = regex.Replace(item.PhoneNumber, m => m.ToString()).Replace(",", "");
                        temp.UserName = regex.Replace(item.UserName, m => m.ToString()).Replace(",", "");
                        temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
                        temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                        csv.Add(temp);
                    }
                    catch (Exception EX)
                    {

                    }
                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "FirstName,LastName,EmailAddress,PhoneNumber,UserName,Note,Date";
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
                var engine = new FileHelperEngine(typeof(ExportStoreUsersOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreUsersOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreUsersInputDto> csv = new List<CreateStoreUsersInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreUsersInputDto temp = new CreateStoreUsersInputDto();
                        temp.FirstName = regex.Replace(item.FirstName, m => m.ToString()).Replace(",", "");
                        temp.LastName = regex.Replace(item.LastName, m => m.ToString()).Replace(",", "");
                        temp.EmailAddress = regex.Replace(item.EmailAddress, m => m.ToString()).Replace(",", "");
                        temp.PhoneNumber = regex.Replace(item.PhoneNumber, m => m.ToString()).Replace(",", "");
                        temp.UserName = regex.Replace(item.UserName, m => m.ToString()).Replace(",", "");
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
                    await _StoreUsersManager
                         .Create(item.MapTo<StoreUsers>());
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}