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
using Storemey.StoreProductVariants.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductVariants
{
    [AbpAuthorize]
    public class StoreProductVariantsAppService : AbpServiceBase, IStoreProductVariantsAppService
    {
        private readonly IStoreProductVariantsManager _StoreProductVariantsManager;
        private readonly IRepository<StoreProductVariants, Guid> _StoreProductVariantsRepository;

        public StoreProductVariantsAppService(
            IStoreProductVariantsManager StoreProductVariantsManager,
            IRepository<StoreProductVariants, Guid> StoreProductVariantsRepository)
        {
            _StoreProductVariantsManager = StoreProductVariantsManager;
            _StoreProductVariantsRepository = StoreProductVariantsRepository;
        }

        public async Task<ListResultDto<GetStoreProductVariantsOutputDto>> ListAll()
        {
            var events = await _StoreProductVariantsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductVariantsOutputDto>>();
            return new ListResultDto<GetStoreProductVariantsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductVariantsInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariants>();
            await _StoreProductVariantsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductVariantsInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariants>();
            await _StoreProductVariantsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductVariantsInputDto input)
        {
            var mapData = input.MapTo<StoreProductVariants>();
            await _StoreProductVariantsManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductVariantsOutputDto> GetById(GetStoreProductVariantsInputDto input)
        {
            var registration = await _StoreProductVariantsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductVariantsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductVariantsOutputDto>> GetAdvanceSearch(StoreProductVariantsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductVariantsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductVariantsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductVariantsOutputDto>(mapDataquery);


        }




     
    }
}