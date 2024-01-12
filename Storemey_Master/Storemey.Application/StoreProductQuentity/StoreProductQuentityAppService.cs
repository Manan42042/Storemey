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
using Storemey.StoreProductQuentity.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductQuentity
{
    [AbpAuthorize]
    public class StoreProductQuentityAppService : AbpServiceBase, IStoreProductQuentityAppService
    {
        private readonly IStoreProductQuentityManager _StoreProductQuentityManager;
        private readonly IRepository<StoreProductQuentity, Guid> _StoreProductQuentityRepository;

        public StoreProductQuentityAppService(
            IStoreProductQuentityManager StoreProductQuentityManager,
            IRepository<StoreProductQuentity, Guid> StoreProductQuentityRepository)
        {
            _StoreProductQuentityManager = StoreProductQuentityManager;
            _StoreProductQuentityRepository = StoreProductQuentityRepository;
        }

        public async Task<ListResultDto<GetStoreProductQuentityOutputDto>> ListAll()
        {
            var events = await _StoreProductQuentityManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductQuentityOutputDto>>();
            return new ListResultDto<GetStoreProductQuentityOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductQuentityInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentity>();
            await _StoreProductQuentityManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductQuentityInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentity>();
            await _StoreProductQuentityManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductQuentityInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentity>();
            await _StoreProductQuentityManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductQuentityOutputDto> GetById(GetStoreProductQuentityInputDto input)
        {
            var registration = await _StoreProductQuentityManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductQuentityOutputDto>();

            return mapData;
        }
        public async Task<List<GetStoreProductQuentityOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId)
        {
            var registration = _StoreProductQuentityManager.ListAllProductAndOutlet(ProductId, OutletId);

            if (registration == null || registration.Result.Count == 0)
            {
                return new List<GetStoreProductQuentityOutputDto>();
            }
            var mapData = registration.Result.MapTo<List<GetStoreProductQuentityOutputDto>>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductQuentityOutputDto>> GetAdvanceSearch(StoreProductQuentityAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductQuentityManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductQuentityOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductQuentityOutputDto>(mapDataquery);


        }




     
    }
}