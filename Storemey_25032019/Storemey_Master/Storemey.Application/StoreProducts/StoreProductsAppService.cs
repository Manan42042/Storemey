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
using Storemey.StoreProducts.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProducts
{
    [AbpAuthorize]
    public class StoreProductsAppService : AbpServiceBase, IStoreProductsAppService
    {
        private readonly IStoreProductsManager _StoreProductsManager;
        private readonly IRepository<StoreProducts, Guid> _StoreProductsRepository;

        public StoreProductsAppService(
            IStoreProductsManager StoreProductsManager,
            IRepository<StoreProducts, Guid> StoreProductsRepository)
        {
            _StoreProductsManager = StoreProductsManager;
            _StoreProductsRepository = StoreProductsRepository;
        }

        public async Task<ListResultDto<GetStoreProductsOutputDto>> ListAll()
        {
            var events = await _StoreProductsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductsOutputDto>>();
            return new ListResultDto<GetStoreProductsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductsInputDto input)
        {
            var mapData = input.MapTo<StoreProducts>();
            await _StoreProductsManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductsOutputDto> GetById(GetStoreProductsInputDto input)
        {
            var registration = await _StoreProductsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductsOutputDto>> GetAdvanceSearch(StoreProductsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreProductsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreProductsManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreProductsOutputDto>(mapDataquery);

        }

    }
}