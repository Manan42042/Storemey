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
using Storemey.StoreSaleItems.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreSaleItems
{
    [AbpAuthorize]
    public class StoreSaleItemsAppService : AbpServiceBase, IStoreSaleItemsAppService
    {
        private readonly IStoreSaleItemsManager _StoreSaleItemsManager;
        private readonly IRepository<StoreSaleItems, Guid> _StoreSaleItemsRepository;

        public StoreSaleItemsAppService(
            IStoreSaleItemsManager StoreSaleItemsManager,
            IRepository<StoreSaleItems, Guid> StoreSaleItemsRepository)
        {
            _StoreSaleItemsManager = StoreSaleItemsManager;
            _StoreSaleItemsRepository = StoreSaleItemsRepository;
        }

        public async Task<ListResultDto<GetStoreSaleItemsOutputDto>> ListAll()
        {
            var events = await _StoreSaleItemsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSaleItemsOutputDto>>();
            return new ListResultDto<GetStoreSaleItemsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreSaleItemsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleItems>();
            await _StoreSaleItemsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreSaleItemsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleItems>();
            await _StoreSaleItemsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreSaleItemsInputDto input)
        {
            var mapData = input.MapTo<StoreSaleItems>();
            await _StoreSaleItemsManager
                .Delete(mapData);
        }


        public async Task<GetStoreSaleItemsOutputDto> GetById(GetStoreSaleItemsInputDto input)
        {
            var registration = await _StoreSaleItemsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreSaleItemsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreSaleItemsOutputDto>> GetAdvanceSearch(StoreSaleItemsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreSaleItemsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreSaleItemsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreSaleItemsManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreSaleItemsOutputDto>(mapDataquery);


        }

    }
}