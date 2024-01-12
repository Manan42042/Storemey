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
using Storemey.StoreProductQuentityHistory.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreProductQuentityHistory
{
    [AbpAuthorize]
    public class StoreProductQuentityHistoryAppService : AbpServiceBase, IStoreProductQuentityHistoryAppService
    {
        private readonly IStoreProductQuentityHistoryManager _StoreProductQuentityHistoryManager;
        private readonly IRepository<StoreProductQuentityHistory, Guid> _StoreProductQuentityHistoryRepository;

        public StoreProductQuentityHistoryAppService(
            IStoreProductQuentityHistoryManager StoreProductQuentityHistoryManager,
            IRepository<StoreProductQuentityHistory, Guid> StoreProductQuentityHistoryRepository)
        {
            _StoreProductQuentityHistoryManager = StoreProductQuentityHistoryManager;
            _StoreProductQuentityHistoryRepository = StoreProductQuentityHistoryRepository;
        }

        public async Task<ListResultDto<GetStoreProductQuentityHistoryOutputDto>> ListAll()
        {
            var events = await _StoreProductQuentityHistoryManager.ListAll();
            var returnData = events.MapTo<List<GetStoreProductQuentityHistoryOutputDto>>();
            return new ListResultDto<GetStoreProductQuentityHistoryOutputDto>(returnData);
        }


        public async Task Create(CreateStoreProductQuentityHistoryInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentityHistory>();
            await _StoreProductQuentityHistoryManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreProductQuentityHistoryInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentityHistory>();
            await _StoreProductQuentityHistoryManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreProductQuentityHistoryInputDto input)
        {
            var mapData = input.MapTo<StoreProductQuentityHistory>();
            await _StoreProductQuentityHistoryManager
                .Delete(mapData);
        }


        public async Task<GetStoreProductQuentityHistoryOutputDto> GetById(GetStoreProductQuentityHistoryInputDto input)
        {
            var registration = await _StoreProductQuentityHistoryManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreProductQuentityHistoryOutputDto>();

            return mapData;
        }

        public async Task<List<GetStoreProductQuentityHistoryOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId)
        {
            var registration = _StoreProductQuentityHistoryManager.ListAllProductAndOutlet(ProductId, OutletId);

            if (registration == null || registration.Result.Count == 0)
            {
                return new List<GetStoreProductQuentityHistoryOutputDto>();
            }
            var mapData = registration.Result.MapTo<List<GetStoreProductQuentityHistoryOutputDto>>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreProductQuentityHistoryOutputDto>> GetAdvanceSearch(StoreProductQuentityHistoryAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreProductQuentityHistoryManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreProductQuentityHistoryOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);

            return new ListResultDto<GetStoreProductQuentityHistoryOutputDto>(mapDataquery);


        }




     
    }
}