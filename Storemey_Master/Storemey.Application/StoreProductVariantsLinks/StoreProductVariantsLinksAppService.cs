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
using Storemey.StoreProductVariantsLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductVariantsLinks
{
    [AbpAuthorize]
    public class StoreProductVariantsLinksAppService : AbpServiceBase, IStoreProductVariantsLinksAppService
    {
        private readonly IStoreProductVariantsLinksManager _StoreProductVariantsLinksManager;
        private readonly IRepository<StoreProductVariantsLinks, Guid> _StoreProductVariantsLinksRepository;

        public StoreProductVariantsLinksAppService(
            IStoreProductVariantsLinksManager StoreProductVariantsLinksManager,
            IRepository<StoreProductVariantsLinks, Guid> StoreProductVariantsLinksRepository)
        {
            _StoreProductVariantsLinksManager = StoreProductVariantsLinksManager;
            _StoreProductVariantsLinksRepository = StoreProductVariantsLinksRepository;
        }

        public async Task<ListResultDto<GetStoreProductVariantsLinksOutputDto>> ListAll()
        {
            var events = await _StoreProductVariantsLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductVariantsLinksOutputDto>>();
            return new ListResultDto<GetStoreProductVariantsLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductVariantsLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantsLinks>();
            await _StoreProductVariantsLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductVariantsLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantsLinks>();
            await _StoreProductVariantsLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductVariantsLinksInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantsLinks>();
            await _StoreProductVariantsLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductVariantsLinksOutputDto> GetById(GetStoreProductVariantsLinksInputDto input)
        {
            var registration = await _StoreProductVariantsLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductVariantsLinksOutputDto>();

            return mapData;
        }
        public async Task<List<GetStoreProductVariantsLinksOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId)
        {
            var registration = _StoreProductVariantsLinksManager.ListAllProductAndOutlet(ProductId, OutletId);

            if (registration == null || registration.Result.Count == 0)
            {
                return new List<GetStoreProductVariantsLinksOutputDto>();
            }
            var mapData = registration.Result.MapTo<List<GetStoreProductVariantsLinksOutputDto>>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductVariantsLinksOutputDto>> GetAdvanceSearch(StoreProductVariantsLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductVariantsLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductVariantsLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductVariantsLinksOutputDto>(mapDataquery);


        }



        public async Task CreateValriantLink(long ProductId, long VariantId, long VariantValueId)
        {
            StoreProductVariantsLinks dto = new StoreProductVariantsLinks();
            dto.ProductId = ProductId;
            dto.VariantId = VariantId;
            dto.VariantValueId = VariantValueId;

            await _StoreProductVariantsLinksManager
                .Create(dto);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductVariantsLinksManager.DeleteByProductId(ProductId);
        }

        public async Task<List<GetStoreProductVariantsLinksOutputDto>> GetByProductId(long ProductId)
        {
            var registration = await _StoreProductVariantsLinksManager.GetByProductId(ProductId);

            var mapData = registration.MapTo<List<GetStoreProductVariantsLinksOutputDto>>();

            return mapData;
        }


    }
}