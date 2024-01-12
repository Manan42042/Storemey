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
using Storemey.StoreInventoryPurchaseLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreInventoryPurchaseLinks
{
    [AbpAuthorize]
    public class StoreInventoryPurchaseLinksAppService : AbpServiceBase, IStoreInventoryPurchaseLinksAppService
    {
        private readonly IStoreInventoryPurchaseLinksManager _StoreInventoryPurchaseLinksManager;
        private readonly IRepository<StoreInventoryPurchaseLinks, Guid> _StoreInventoryPurchaseLinksRepository;

        public StoreInventoryPurchaseLinksAppService(
            IStoreInventoryPurchaseLinksManager StoreInventoryPurchaseLinksManager,
            IRepository<StoreInventoryPurchaseLinks, Guid> StoreInventoryPurchaseLinksRepository)
        {
            _StoreInventoryPurchaseLinksManager = StoreInventoryPurchaseLinksManager;
            _StoreInventoryPurchaseLinksRepository = StoreInventoryPurchaseLinksRepository;
        }

        public async Task<ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>> ListAll()
        {
            var events = await _StoreInventoryPurchaseLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreInventoryPurchaseLinksOutputDto>>();
            return new ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreInventoryPurchaseLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseLinks>();
            await _StoreInventoryPurchaseLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreInventoryPurchaseLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseLinks>();
            await _StoreInventoryPurchaseLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreInventoryPurchaseLinksInputDto input)
        {
            var mapData = input.MapTo<StoreInventoryPurchaseLinks>();
            await _StoreInventoryPurchaseLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreInventoryPurchaseLinksOutputDto> GetById(GetStoreInventoryPurchaseLinksInputDto input)
        {
            var registration = await _StoreInventoryPurchaseLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreInventoryPurchaseLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>> GetAdvanceSearch(StoreInventoryPurchaseLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreInventoryPurchaseLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreInventoryPurchaseLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>(mapDataquery);

        }


    }
}