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
using Storemey.StoreProductTagLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductTagLinks
{
    [AbpAuthorize]
    public class StoreProductTagLinksAppService : AbpServiceBase, IStoreProductTagLinksAppService
    {
        private readonly IStoreProductTagLinksManager _StoreProductTagLinksManager;
        private readonly IRepository<StoreProductTagLinks, Guid> _StoreProductTagLinksRepository;

        public StoreProductTagLinksAppService(
            IStoreProductTagLinksManager StoreProductTagLinksManager,
            IRepository<StoreProductTagLinks, Guid> StoreProductTagLinksRepository)
        {
            _StoreProductTagLinksManager = StoreProductTagLinksManager;
            _StoreProductTagLinksRepository = StoreProductTagLinksRepository;
        }

        public async Task<ListResultDto<GetStoreProductTagLinksOutputDto>> ListAll()
        {
            var events = await _StoreProductTagLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductTagLinksOutputDto>>();
            return new ListResultDto<GetStoreProductTagLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductTagLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductTagLinks>();
            await _StoreProductTagLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductTagLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductTagLinks>();
            await _StoreProductTagLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductTagLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductTagLinks>();
            await _StoreProductTagLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductTagLinksOutputDto> GetById(GetStoreProductTagLinksInputDto input)
        {
            var registration = await _StoreProductTagLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductTagLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductTagLinksOutputDto>> GetAdvanceSearch(StoreProductTagLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductTagLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductTagLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductTagLinksOutputDto>(mapDataquery);


        }

        public async Task CreateProductLinks(long ProductId, long Id)
        {
            StoreProductTagLinks dto = new StoreProductTagLinks();
            dto.ProductId = ProductId;
            dto.TagId = Id;

            await _StoreProductTagLinksManager
                .Create(dto);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductTagLinksManager.DeleteByProductId(ProductId);
        }

        public async Task<List<GetStoreProductTagLinksOutputDto>> GetByProductId(long ProductId)
        {
            var registration = await _StoreProductTagLinksManager.GetByProductId(ProductId);

            var mapData = registration.MapTo<List<GetStoreProductTagLinksOutputDto>>();

            return mapData;
        }


    }
}