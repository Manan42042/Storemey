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
using Storemey.StoreInventory.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreInventory
{
    [AbpAuthorize]
    public class StoreInventoryAppService : AbpServiceBase, IStoreInventoryAppService
    {
        private readonly IStoreInventoryManager _StoreInventoryManager;
        private readonly IRepository<StoreInventory, Guid> _StoreInventoryRepository;

        public StoreInventoryAppService(
            IStoreInventoryManager StoreInventoryManager,
            IRepository<StoreInventory, Guid> StoreInventoryRepository)
        {
            _StoreInventoryManager = StoreInventoryManager;
            _StoreInventoryRepository = StoreInventoryRepository;
        }

        public async Task<ListResultDto<GetStoreInventoryOutputDto>> ListAll()
        {
            var events = await _StoreInventoryManager.ListAll();
            var returnData = events.MapTo<List<GetStoreInventoryOutputDto>>();
            return new ListResultDto<GetStoreInventoryOutputDto>(returnData);
        }

        public async Task<List<GetStoreInventoryOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId)
        {
            var registration = _StoreInventoryManager.ListAllProductAndOutlet(ProductId, OutletId);

            if (registration == null || registration.Result.Count == 0)
            {
                return new List<GetStoreInventoryOutputDto>();
            }
            var mapData = registration.Result.MapTo<List<GetStoreInventoryOutputDto>>();

            return mapData;
        }


        public async Task Create(CreateStoreInventoryInputDto input)
        {
            var mapData = input.MapTo<StoreInventory>();
            await _StoreInventoryManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreInventoryInputDto input)
        {
            var mapData = input.MapTo<StoreInventory>();
            await _StoreInventoryManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreInventoryInputDto input)
        {
            var mapData = input.MapTo<StoreInventory>();
            await _StoreInventoryManager
                .Delete(mapData);
        }

        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreInventoryManager.DeleteByProductId(ProductId);
        }

        public async Task<GetStoreInventoryOutputDto> GetById(GetStoreInventoryInputDto input)
        {
            var registration = await _StoreInventoryManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreInventoryOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreInventoryOutputDto>> GetAdvanceSearch(StoreInventoryAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreInventoryManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreInventoryOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreInventoryOutputDto>(mapDataquery);


        }


    
    }
}