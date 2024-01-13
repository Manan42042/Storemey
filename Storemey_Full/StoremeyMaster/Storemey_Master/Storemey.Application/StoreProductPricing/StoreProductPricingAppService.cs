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
using Storemey.StoreProductPricing.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductPricing
{
    [AbpAuthorize]
    public class StoreProductPricingAppService : AbpServiceBase, IStoreProductPricingAppService
    {
        private readonly IStoreProductPricingManager _StoreProductPricingManager;
        private readonly IRepository<StoreProductPricing, Guid> _StoreProductPricingRepository;

        public StoreProductPricingAppService(
            IStoreProductPricingManager StoreProductPricingManager,
            IRepository<StoreProductPricing, Guid> StoreProductPricingRepository)
        {
            _StoreProductPricingManager = StoreProductPricingManager;
            _StoreProductPricingRepository = StoreProductPricingRepository;
        }

        public async Task<ListResultDto<GetStoreProductPricingOutputDto>> ListAll()
        {
            var events = await _StoreProductPricingManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductPricingOutputDto>>();
            return new ListResultDto<GetStoreProductPricingOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductPricingInputDto input)
        {
            var mapData = input.MapTo<StoreProductPricing>();
            await _StoreProductPricingManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductPricingInputDto input)
        {
            var mapData = input.MapTo<StoreProductPricing>();
            await _StoreProductPricingManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductPricingInputDto input)
        {
            var mapData = input.MapTo<StoreProductPricing>();
            await _StoreProductPricingManager
                .Delete(mapData);
        }
        public async Task DeleteByProductId(long ProductId)
        {
            await _StoreProductPricingManager.DeleteByProductId(ProductId);
        }


        public async Task<GetStoreProductPricingOutputDto> GetById(GetStoreProductPricingInputDto input)
        {
            var registration = await _StoreProductPricingManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductPricingOutputDto>();

            return mapData;
        }



        public async Task<List<GetStoreProductPricingOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId)
        {
            var registration = _StoreProductPricingManager.ListAllProductAndOutlet(ProductId,OutletId);

            if (registration == null || registration.Result.Count == 0)
            {
                return new List<GetStoreProductPricingOutputDto>();
            }
            var mapData = registration.Result.MapTo<List<GetStoreProductPricingOutputDto>>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductPricingOutputDto>> GetAdvanceSearch(StoreProductPricingAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductPricingManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductPricingOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductPricingOutputDto>(mapDataquery);


        }





    }
}