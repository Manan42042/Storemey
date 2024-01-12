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
using Storemey.StoreInventoryPurchaseOrders.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreInventoryPurchaseOrders
{
    [AbpAuthorize]
    public class StoreInventoryPurchaseOrdersAppService : AbpServiceBase, IStoreInventoryPurchaseOrdersAppService
    {
        private readonly IStoreInventoryPurchaseOrdersManager _StoreInventoryPurchaseOrdersManager;
        private readonly IRepository<StoreInventoryPurchaseOrders, Guid> _StoreInventoryPurchaseOrdersRepository;

        public StoreInventoryPurchaseOrdersAppService(
            IStoreInventoryPurchaseOrdersManager StoreInventoryPurchaseOrdersManager,
            IRepository<StoreInventoryPurchaseOrders, Guid> StoreInventoryPurchaseOrdersRepository)
        {
            _StoreInventoryPurchaseOrdersManager = StoreInventoryPurchaseOrdersManager;
            _StoreInventoryPurchaseOrdersRepository = StoreInventoryPurchaseOrdersRepository;
        }

        public async Task<ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>> ListAll()
        {
            var events = await _StoreInventoryPurchaseOrdersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreInventoryPurchaseOrdersOutputDto>>();
            return new ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreInventoryPurchaseOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseOrders>();
            await _StoreInventoryPurchaseOrdersManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreInventoryPurchaseOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseOrders>();
            await _StoreInventoryPurchaseOrdersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreInventoryPurchaseOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseOrders>();
            await _StoreInventoryPurchaseOrdersManager
                .Delete(mapData);
        }


        public async Task<GetStoreInventoryPurchaseOrdersOutputDto> GetById(GetStoreInventoryPurchaseOrdersInputDto input)
        {
            var registration = await _StoreInventoryPurchaseOrdersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreInventoryPurchaseOrdersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>> GetAdvanceSearch(StoreInventoryPurchaseOrdersAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreInventoryPurchaseOrdersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreInventoryPurchaseOrdersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>(mapDataquery);


        }


    }
}