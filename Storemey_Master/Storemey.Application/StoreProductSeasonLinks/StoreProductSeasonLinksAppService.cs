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
using Storemey.StoreProductSeasonLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductSeasonLinks
{
    [AbpAuthorize]
    public class StoreProductSeasonLinksAppService : AbpServiceBase, IStoreProductSeasonLinksAppService
    {
        private readonly IStoreProductSeasonLinksManager _StoreProductSeasonLinksManager;
        private readonly IRepository<StoreProductSeasonLinks, Guid> _StoreProductSeasonLinksRepository;

        public StoreProductSeasonLinksAppService(
            IStoreProductSeasonLinksManager StoreProductSeasonLinksManager,
            IRepository<StoreProductSeasonLinks, Guid> StoreProductSeasonLinksRepository)
        {
            _StoreProductSeasonLinksManager = StoreProductSeasonLinksManager;
            _StoreProductSeasonLinksRepository = StoreProductSeasonLinksRepository;
        }

        public async Task<ListResultDto<GetStoreProductSeasonLinksOutputDto>> ListAll()
        {
            var events = await _StoreProductSeasonLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductSeasonLinksOutputDto>>();
            return new ListResultDto<GetStoreProductSeasonLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductSeasonLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductSeasonLinks>();
            await _StoreProductSeasonLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductSeasonLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductSeasonLinks>();
            await _StoreProductSeasonLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductSeasonLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductSeasonLinks>();
            await _StoreProductSeasonLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductSeasonLinksOutputDto> GetById(GetStoreProductSeasonLinksInputDto input)
        {
            var registration = await _StoreProductSeasonLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductSeasonLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductSeasonLinksOutputDto>> GetAdvanceSearch(StoreProductSeasonLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductSeasonLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductSeasonLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductSeasonLinksOutputDto>(mapDataquery);

        }



        public async Task CreateProductLinks(long ProductId, long Id)
        {
            StoreProductSeasonLinks dto = new StoreProductSeasonLinks();
            dto.ProductId = ProductId;
            dto.SeasonId = Id;

            await _StoreProductSeasonLinksManager
                .Create(dto);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductSeasonLinksManager.DeleteByProductId(ProductId);
        }

        public async Task<List<GetStoreProductSeasonLinksOutputDto>> GetByProductId(long ProductId)
        {
            var registration = await _StoreProductSeasonLinksManager.GetByProductId(ProductId);

            var mapData = registration.MapTo<List<GetStoreProductSeasonLinksOutputDto>>();

            return mapData;
        }

    }
}