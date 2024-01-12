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
using Storemey.StoreProductCategoryLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductCategoryLinks
{
    [AbpAuthorize]
    public class StoreProductCategoryLinksAppService : AbpServiceBase, IStoreProductCategoryLinksAppService
    {
        private readonly IStoreProductCategoryLinksManager _StoreProductCategoryLinksManager;
        private readonly IRepository<StoreProductCategoryLinks, Guid> _StoreProductCategoryLinksRepository;

        public StoreProductCategoryLinksAppService(
            IStoreProductCategoryLinksManager StoreProductCategoryLinksManager,
            IRepository<StoreProductCategoryLinks, Guid> StoreProductCategoryLinksRepository)
        {
            _StoreProductCategoryLinksManager = StoreProductCategoryLinksManager;
            _StoreProductCategoryLinksRepository = StoreProductCategoryLinksRepository;
        }

        public async Task<ListResultDto<GetStoreProductCategoryLinksOutputDto>> ListAll()
        {
            var events = await _StoreProductCategoryLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductCategoryLinksOutputDto>>();
            return new ListResultDto<GetStoreProductCategoryLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductCategoryLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductCategoryLinks>();
            await _StoreProductCategoryLinksManager
                .Create(mapData);
        }

       
        public async Task Update(UpdateStoreProductCategoryLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductCategoryLinks>();
            await _StoreProductCategoryLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductCategoryLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductCategoryLinks>();
            await _StoreProductCategoryLinksManager
                .Delete(mapData);
        }

      


        public async Task<GetStoreProductCategoryLinksOutputDto> GetById(GetStoreProductCategoryLinksInputDto input)
        {
            var registration = await _StoreProductCategoryLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductCategoryLinksOutputDto>();

            return mapData;
        }


        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductCategoryLinksOutputDto>> GetAdvanceSearch(StoreProductCategoryLinksAdvanceSearchInputDto input)
        {
            var filtereddatatquery = await _StoreProductCategoryLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductCategoryLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductCategoryLinksOutputDto>(mapDataquery);

        }



        public async Task CreateProductLinks(long ProductId, long Id)
        {
            StoreProductCategoryLinks dto = new StoreProductCategoryLinks();
            dto.ProductId = ProductId;
            dto.CategoryId = Id;

            await _StoreProductCategoryLinksManager
                .Create(dto);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductCategoryLinksManager.DeleteByProductId(ProductId);
        }

        public async Task<List<GetStoreProductCategoryLinksOutputDto>> GetByProductId(long ProductId)
        {
            var registration = await _StoreProductCategoryLinksManager.GetByProductId(ProductId);

            var mapData = registration.MapTo<List<GetStoreProductCategoryLinksOutputDto>>();

            return mapData;
        }
    }
}