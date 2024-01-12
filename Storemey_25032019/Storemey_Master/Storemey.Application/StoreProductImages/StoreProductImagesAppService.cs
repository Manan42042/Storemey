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
using Storemey.StoreProductImages.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductImages
{
    [AbpAuthorize]
    public class StoreProductImagesAppService : AbpServiceBase, IStoreProductImagesAppService
    {
        private readonly IStoreProductImagesManager _StoreProductImagesManager;
        private readonly IRepository<StoreProductImages, Guid> _StoreProductImagesRepository;

        public StoreProductImagesAppService(
            IStoreProductImagesManager StoreProductImagesManager,
            IRepository<StoreProductImages, Guid> StoreProductImagesRepository)
        {
            _StoreProductImagesManager = StoreProductImagesManager;
            _StoreProductImagesRepository = StoreProductImagesRepository;
        }

        public async Task<ListResultDto<GetStoreProductImagesOutputDto>> ListAll()
        {
            var events = await _StoreProductImagesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductImagesOutputDto>>();
            return new ListResultDto<GetStoreProductImagesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductImagesInputDto input)
        {
            var mapData = input.MapTo<StoreProductImages>();
            await _StoreProductImagesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductImagesInputDto input)
        {
            var mapData = input.MapTo<StoreProductImages>();
            await _StoreProductImagesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductImagesInputDto input)
        {
            var mapData = input.MapTo<StoreProductImages>();
            await _StoreProductImagesManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductImagesOutputDto> GetById(GetStoreProductImagesInputDto input)
        {
            var registration = await _StoreProductImagesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductImagesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductImagesOutputDto>> GetAdvanceSearch(StoreProductImagesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductImagesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreProductImagesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreProductImagesManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreProductImagesOutputDto>(mapDataquery);


        }


    }
}