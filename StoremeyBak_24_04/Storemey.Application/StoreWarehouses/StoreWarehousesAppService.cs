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
using Storemey.StoreWarehouses.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreWarehouses
{
    [AbpAuthorize]
    public class StoreWarehousesAppService : AbpServiceBase, IStoreWarehousesAppService
    {
        private readonly IStoreWarehousesManager _StoreWarehousesManager;
        private readonly IRepository<StoreWarehouses, Guid> _StoreWarehousesRepository;

        public StoreWarehousesAppService(
            IStoreWarehousesManager StoreWarehousesManager,
            IRepository<StoreWarehouses, Guid> StoreWarehousesRepository)
        {
            _StoreWarehousesManager = StoreWarehousesManager;
            _StoreWarehousesRepository = StoreWarehousesRepository;
        }

        public async Task<ListResultDto<GetStoreWarehousesOutputDto>> ListAll()
        {
            var events = await _StoreWarehousesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreWarehousesOutputDto>>();
            return new ListResultDto<GetStoreWarehousesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreWarehousesInputDto input)
        {
            var mapData = input.MapTo<StoreWarehouses>();
            await _StoreWarehousesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreWarehousesInputDto input)
        {
            var mapData = input.MapTo<StoreWarehouses>();
            await _StoreWarehousesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreWarehousesInputDto input)
        {
            var mapData = input.MapTo<StoreWarehouses>();
            await _StoreWarehousesManager
                .Delete(mapData);
        }


        public async Task<GetStoreWarehousesOutputDto> GetById(GetStoreWarehousesInputDto input)
        {
            var registration = await _StoreWarehousesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreWarehousesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreWarehousesOutputDto>> GetAdvanceSearch(StoreWarehousesAdvanceSearchInputDto input)
        {
            try
            {
                var filtereddatatquery = await _StoreWarehousesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreWarehousesOutputDto>>();
                mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
                return new ListResultDto<GetStoreWarehousesOutputDto>(mapDataquery);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}