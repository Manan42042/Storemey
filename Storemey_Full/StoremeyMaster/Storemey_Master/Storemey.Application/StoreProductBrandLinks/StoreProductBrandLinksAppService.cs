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
using Storemey.StoreProductBrandLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductBrandLinks
{
    [AbpAuthorize]
    public class StoreProductBrandLinksAppService : AbpServiceBase, IStoreProductBrandLinksAppService
    {
        private readonly IStoreProductBrandLinksManager _StoreProductBrandLinksManager;
        private readonly IRepository<StoreProductBrandLinks, Guid> _StoreProductBrandLinksRepository;

        public StoreProductBrandLinksAppService(
            IStoreProductBrandLinksManager StoreProductBrandLinksManager,
            IRepository<StoreProductBrandLinks, Guid> StoreProductBrandLinksRepository)
        {
            _StoreProductBrandLinksManager = StoreProductBrandLinksManager;
            _StoreProductBrandLinksRepository = StoreProductBrandLinksRepository;
        }

        public async Task<ListResultDto<GetStoreProductBrandLinksOutputDto>> ListAll()
        {
            var events = await _StoreProductBrandLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductBrandLinksOutputDto>>();
            return new ListResultDto<GetStoreProductBrandLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductBrandLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductBrandLinks>();
            await _StoreProductBrandLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductBrandLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductBrandLinks>();
            await _StoreProductBrandLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductBrandLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductBrandLinks>();
            await _StoreProductBrandLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductBrandLinksOutputDto> GetById(GetStoreProductBrandLinksInputDto input)
        {
            var registration = await _StoreProductBrandLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductBrandLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductBrandLinksOutputDto>> GetAdvanceSearch(StoreProductBrandLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductBrandLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductBrandLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductBrandLinksOutputDto>(mapDataquery);


        }




        public async Task CreateProductLinks(long ProductId, long Id)
        {
            StoreProductBrandLinks dto = new StoreProductBrandLinks();
            dto.ProductId = ProductId;
            dto.BrandId = Id;

            await _StoreProductBrandLinksManager
                .Create(dto);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductBrandLinksManager.DeleteByProductId(ProductId);
        }

        public async Task<List<GetStoreProductBrandLinksOutputDto>> GetByProductId(long ProductId)
        {
            var registration = await _StoreProductBrandLinksManager.GetByProductId(ProductId);

            var mapData = registration.MapTo<List<GetStoreProductBrandLinksOutputDto>>();

            return mapData;
        }

    }
}