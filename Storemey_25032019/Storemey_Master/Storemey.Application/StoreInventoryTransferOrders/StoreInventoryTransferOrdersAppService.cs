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
using Storemey.StoreInventoryTransferOrders.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreInventoryTransferOrders
{
    [AbpAuthorize]
    public class StoreInventoryTransferOrdersAppService : AbpServiceBase, IStoreInventoryTransferOrdersAppService
    {
        private readonly IStoreInventoryTransferOrdersManager _StoreInventoryTransferOrdersManager;
        private readonly IRepository<StoreInventoryTransferOrders, Guid> _StoreInventoryTransferOrdersRepository;

        public StoreInventoryTransferOrdersAppService(
            IStoreInventoryTransferOrdersManager StoreInventoryTransferOrdersManager,
            IRepository<StoreInventoryTransferOrders, Guid> StoreInventoryTransferOrdersRepository)
        {
            _StoreInventoryTransferOrdersManager = StoreInventoryTransferOrdersManager;
            _StoreInventoryTransferOrdersRepository = StoreInventoryTransferOrdersRepository;
        }

        public async Task<ListResultDto<GetStoreInventoryTransferOrdersOutputDto>> ListAll()
        {
            var events = await _StoreInventoryTransferOrdersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreInventoryTransferOrdersOutputDto>>();
            return new ListResultDto<GetStoreInventoryTransferOrdersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreInventoryTransferOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferOrders>();
            await _StoreInventoryTransferOrdersManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreInventoryTransferOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferOrders>();
            await _StoreInventoryTransferOrdersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreInventoryTransferOrdersInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferOrders>();
            await _StoreInventoryTransferOrdersManager
                .Delete(mapData);
        }


        public async Task<GetStoreInventoryTransferOrdersOutputDto> GetById(GetStoreInventoryTransferOrdersInputDto input)
        {
            var registration = await _StoreInventoryTransferOrdersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreInventoryTransferOrdersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreInventoryTransferOrdersOutputDto>> GetAdvanceSearch(StoreInventoryTransferOrdersAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreInventoryTransferOrdersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreInventoryTransferOrdersOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreInventoryTransferOrdersManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreInventoryTransferOrdersOutputDto>(mapDataquery);


        }


    }
}