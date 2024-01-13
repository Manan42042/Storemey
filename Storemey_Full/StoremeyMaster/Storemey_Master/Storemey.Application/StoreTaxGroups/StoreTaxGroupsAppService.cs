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
using Storemey.StoreTaxGroups.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.StoreTax;
using Storemey.StoreTaxGroupLinks;

namespace Storemey.StoreTaxGroups
{
    [AbpAuthorize]
    public class StoreTaxGroupsAppService : AbpServiceBase, IStoreTaxGroupsAppService
    {
        private readonly IStoreTaxGroupsManager _StoreTaxGroupsManager;
        private readonly IStoreTaxGroupLinksManager _StoreTaxGroupLinksManager;
        private readonly IStoreTaxAppService _StoreTaxManager;
        private readonly IRepository<StoreTaxGroups, Guid> _StoreTaxGroupsRepository;

        public StoreTaxGroupsAppService(
            IStoreTaxGroupsManager StoreTaxGroupsManager,
            IStoreTaxGroupLinksManager StoreTaxGroupLinksManager,
            IStoreTaxAppService StoreTaxManager,
            IRepository<StoreTaxGroups, Guid> StoreTaxGroupsRepository)
        {
            _StoreTaxGroupsManager = StoreTaxGroupsManager;
            _StoreTaxGroupsRepository = StoreTaxGroupsRepository;
            _StoreTaxManager = StoreTaxManager;
            _StoreTaxGroupLinksManager = StoreTaxGroupLinksManager;
        }

        public async Task<ListResultDto<GetStoreTaxGroupsOutputDto>> ListAll()
        {
            var events = await _StoreTaxGroupsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxGroupsOutputDto>>();
            return new ListResultDto<GetStoreTaxGroupsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreTaxGroupsInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroups>();
            await _StoreTaxGroupsManager
                .Create(mapData);

            string commaSepareted = string.Empty;
            int groupRate = 0;

            foreach (var item in input.listTax)
            {
                if (item.Isassign)
                {
                    StoreTaxGroupLinks.StoreTaxGroupLinks STP = new StoreTaxGroupLinks.StoreTaxGroupLinks();
                    STP.TaxGroupId = Convert.ToInt32(mapData.Id);
                    STP.TaxId = Convert.ToInt32(item.Id);
                    await _StoreTaxGroupLinksManager.Create(STP);

                    commaSepareted += item.TaxName + ",";
                    groupRate += item.Rate;
                }
            }
            commaSepareted = commaSepareted.TrimEnd(',');
            mapData.TaxCommaseparated = commaSepareted;
            mapData.Rate = groupRate;


            await _StoreTaxGroupsManager
              .Update(mapData);
        }

        public async Task Update(UpdateStoreTaxGroupsInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroups>();
            await _StoreTaxGroupsManager
                .Update(mapData);
            await _StoreTaxGroupLinksManager.DeleteByGroupID(Convert.ToInt32(mapData.Id));


            string commaSepareted = string.Empty;
            int groupRate = 0;



            foreach (var item in input.ListTax)
            {
                if (item.Isassign)
                {
                    StoreTaxGroupLinks.StoreTaxGroupLinks STP = new StoreTaxGroupLinks.StoreTaxGroupLinks();
                    STP.TaxGroupId = Convert.ToInt32(mapData.Id);
                    STP.TaxId = Convert.ToInt32(item.Id);
                    await _StoreTaxGroupLinksManager.Create(STP);


                    commaSepareted += item.TaxName + ",";
                    groupRate += item.Rate;
                }
            }
            commaSepareted = commaSepareted.TrimEnd(',');
            mapData.TaxCommaseparated = commaSepareted;
            mapData.Rate = groupRate;


            await _StoreTaxGroupsManager
              .Update(mapData);
        }

        public async Task Delete(DeleteStoreTaxGroupsInputDto input)
        {
            var mapData = input.MapTo<StoreTaxGroups>();
            await _StoreTaxGroupsManager
                .Delete(mapData);
        }


        public async Task<GetStoreTaxGroupsOutputDto> GetById(GetStoreTaxGroupsInputDto input)
        {
            var registration = await _StoreTaxGroupsManager.GetByID(input.Id);



            var mapData = registration.MapTo<GetStoreTaxGroupsOutputDto>();
            if (mapData != null)
            {
                mapData.listTax = await _StoreTaxManager.GetByGroupId(Convert.ToInt32(input.Id));
            }
            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreTaxGroupsOutputDto>> GetAdvanceSearch(StoreTaxGroupsAdvanceSearchInputDto input)
        {




            var filtereddatatquery = await _StoreTaxGroupsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreTaxGroupsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetStoreTaxGroupsOutputDto>(mapDataquery);


        }


        public async Task<string> ExportToCSV()
        {
            var events = await _StoreTaxGroupsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTaxGroupsOutputDto>>();
            string Filename = ExportToCSVFile(returnData);
            return Filename;
        }



        public string ExportToCSVFile(List<GetStoreTaxGroupsOutputDto> dataSource)
        {
            try
            {
                FileHelperEngine engine = new FileHelperEngine(typeof(ExportStoreTaxGroupsOutputDto));
                List<ExportStoreTaxGroupsOutputDto> csv = new List<ExportStoreTaxGroupsOutputDto>();
                foreach (var item in dataSource)
                {
                    string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                    var regex = new Regex(exp);

                    //                    public virtual string TaxGroupName { get; set; }
                    //public virtual string TaxCommaseparated { get; set; }
                    //public virtual string Note { get; set; }


                    ExportStoreTaxGroupsOutputDto temp = new ExportStoreTaxGroupsOutputDto();
                    temp.TaxGroupName = regex.Replace(item.TaxGroupName, m => m.ToString()).Replace(",", "");
                    temp.Note = regex.Replace(item.Note, m => m.ToString()).Replace(",", "");
                    temp.Date = Convert.ToDateTime(item.LastModificationTime).ToString("MM-dd-yyyy");
                    csv.Add(temp);

                }
                string filename = Guid.NewGuid() + ".csv";
                engine.HeaderText = "TaxGroupName,Note,Date";
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
                var engine = new FileHelperEngine(typeof(ExportStoreTaxGroupsOutputDto));
                //read the CSV file into your object Arrary
                var records = (ExportStoreTaxGroupsOutputDto[])engine.ReadFile(System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/") + fileName);
                List<CreateStoreTaxGroupsInputDto> csv = new List<CreateStoreTaxGroupsInputDto>();

                if (records.Any())
                {
                    //process your records as per your requirements
                    foreach (var item in records)
                    {
                        string exp = "(?:^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
                        var regex = new Regex(exp);

                        CreateStoreTaxGroupsInputDto temp = new CreateStoreTaxGroupsInputDto();
                        temp.Id = item.Id;
                        temp.TaxGroupName = regex.Replace(item.TaxGroupName, m => m.ToString()).Replace(",", "");
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
                    await _StoreTaxGroupsManager
                         .Create(item.MapTo<StoreTaxGroups>());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}