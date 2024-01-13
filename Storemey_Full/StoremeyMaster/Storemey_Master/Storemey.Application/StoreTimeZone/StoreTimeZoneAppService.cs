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
using Storemey.StoreTimeZones.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreTimeZones
{
    [AbpAuthorize]
    public class StoreTimeZonesAppService : AbpServiceBase, IStoreTimeZonesAppService
    {
        private readonly IStoreTimeZonesManager _StoreTimeZonesManager;
        private readonly IRepository<StoreTimeZones, Guid> _StoreTimeZonesRepository;

        public StoreTimeZonesAppService(
            IStoreTimeZonesManager StoreTimeZonesManager,
            IRepository<StoreTimeZones, Guid> StoreTimeZonesRepository)
        {
            _StoreTimeZonesManager = StoreTimeZonesManager;
            _StoreTimeZonesRepository = StoreTimeZonesRepository;
        }

        public async Task<ListResultDto<GetStoreTimeZonesOutputDto>> ListAll()
        {
            var events = await _StoreTimeZonesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreTimeZonesOutputDto>>();

            returnData.ForEach(x => x.name = "(" + x.current_utc_offset + ") " + x.name);

            return new ListResultDto<GetStoreTimeZonesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreTimeZonesInputDto input)
        {
            var mapData = input.MapTo<StoreTimeZones>();
            await _StoreTimeZonesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreTimeZonesInputDto input)
        {
            var mapData = input.MapTo<StoreTimeZones>();
            await _StoreTimeZonesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreTimeZonesInputDto input)
        {
            var mapData = input.MapTo<StoreTimeZones>();
            await _StoreTimeZonesManager
                .Delete(mapData);
        }


        public async Task<GetStoreTimeZonesOutputDto> GetById(GetStoreTimeZonesInputDto input)
        {
            var registration = await _StoreTimeZonesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreTimeZonesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreTimeZonesOutputDto>> GetAdvanceSearch(StoreTimeZonesAdvanceSearchInputDto input)
        {
            try
            {
                var filtereddatatquery = await _StoreTimeZonesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreTimeZonesOutputDto>>();
                mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
                return new ListResultDto<GetStoreTimeZonesOutputDto>(mapDataquery);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}