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
using Storemey.StoreProductVariantValues.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductVariantValues
{
    [AbpAuthorize]
    public class StoreProductVariantValuesAppService : AbpServiceBase, IStoreProductVariantValuesAppService
    {
        private readonly IStoreProductVariantValuesManager _StoreProductVariantValuesManager;
        private readonly IRepository<StoreProductVariantValues, Guid> _StoreProductVariantValuesRepository;

        public StoreProductVariantValuesAppService(
            IStoreProductVariantValuesManager StoreProductVariantValuesManager,
            IRepository<StoreProductVariantValues, Guid> StoreProductVariantValuesRepository)
        {
            _StoreProductVariantValuesManager = StoreProductVariantValuesManager;
            _StoreProductVariantValuesRepository = StoreProductVariantValuesRepository;
        }

        public async Task<ListResultDto<GetStoreProductVariantValuesOutputDto>> ListAll()
        {
            var events = await _StoreProductVariantValuesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductVariantValuesOutputDto>>();
            return new ListResultDto<GetStoreProductVariantValuesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductVariantValuesInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantValues>();
            await _StoreProductVariantValuesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductVariantValuesInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantValues>();
            await _StoreProductVariantValuesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductVariantValuesInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariantValues>();
            await _StoreProductVariantValuesManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductVariantValuesOutputDto> GetById(GetStoreProductVariantValuesInputDto input)
        {
            var registration = await _StoreProductVariantValuesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductVariantValuesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductVariantValuesOutputDto>> GetAdvanceSearch(StoreProductVariantValuesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductVariantValuesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.MapTo<List<GetStoreProductVariantValuesOutputDto>>();
            //mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductVariantValuesOutputDto>(mapDataquery);


        }




     
    }
}