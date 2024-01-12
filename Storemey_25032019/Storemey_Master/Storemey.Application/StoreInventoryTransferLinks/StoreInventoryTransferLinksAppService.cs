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
using Storemey.StoreInventoryTransferLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreInventoryTransferLinks
{
    [AbpAuthorize]
    public class StoreInventoryTransferLinksAppService : AbpServiceBase, IStoreInventoryTransferLinksAppService
    {
        private readonly IStoreInventoryTransferLinksManager _StoreInventoryTransferLinksManager;
        private readonly IRepository<StoreInventoryTransferLinks, Guid> _StoreInventoryTransferLinksRepository;

        public StoreInventoryTransferLinksAppService(
            IStoreInventoryTransferLinksManager StoreInventoryTransferLinksManager,
            IRepository<StoreInventoryTransferLinks, Guid> StoreInventoryTransferLinksRepository)
        {
            _StoreInventoryTransferLinksManager = StoreInventoryTransferLinksManager;
            _StoreInventoryTransferLinksRepository = StoreInventoryTransferLinksRepository;
        }

        public async Task<ListResultDto<GetStoreInventoryTransferLinksOutputDto>> ListAll()
        {
            var events = await _StoreInventoryTransferLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreInventoryTransferLinksOutputDto>>();
            return new ListResultDto<GetStoreInventoryTransferLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreInventoryTransferLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferLinks>();
            await _StoreInventoryTransferLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreInventoryTransferLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferLinks>();
            await _StoreInventoryTransferLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreInventoryTransferLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryTransferLinks>();
            await _StoreInventoryTransferLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreInventoryTransferLinksOutputDto> GetById(GetStoreInventoryTransferLinksInputDto input)
        {
            var registration = await _StoreInventoryTransferLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreInventoryTransferLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreInventoryTransferLinksOutputDto>> GetAdvanceSearch(StoreInventoryTransferLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreInventoryTransferLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreInventoryTransferLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreInventoryTransferLinksManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreInventoryTransferLinksOutputDto>(mapDataquery);

        }

    }
}