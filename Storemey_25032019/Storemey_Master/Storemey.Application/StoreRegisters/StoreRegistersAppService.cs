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
using Storemey.StoreRegisters.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreRegisters
{
    [AbpAuthorize]
    public class StoreRegistersAppService : AbpServiceBase, IStoreRegistersAppService
    {
        private readonly IStoreRegistersManager _StoreRegistersManager;
        private readonly IRepository<StoreRegisters, Guid> _StoreRegistersRepository;

        public StoreRegistersAppService(
            IStoreRegistersManager StoreRegistersManager,
            IRepository<StoreRegisters, Guid> StoreRegistersRepository)
        {
            _StoreRegistersManager = StoreRegistersManager;
            _StoreRegistersRepository = StoreRegistersRepository;
        }

        public async Task<ListResultDto<GetStoreRegistersOutputDto>> ListAll()
        {
            var events = await _StoreRegistersManager.ListAll();
            var returnData = events.MapTo<List<GetStoreRegistersOutputDto>>();
            return new ListResultDto<GetStoreRegistersOutputDto>(returnData);
        }


        public async Task Create(CreateStoreRegistersInputDto input)
        {
            var mapData = input.MapTo<StoreRegisters>();
            await _StoreRegistersManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreRegistersInputDto input)
        {
            var mapData = input.MapTo<StoreRegisters>();
            await _StoreRegistersManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreRegistersInputDto input)
        {
            var mapData = input.MapTo<StoreRegisters>();
            await _StoreRegistersManager
                .Delete(mapData);
        }


        public async Task<GetStoreRegistersOutputDto> GetById(GetStoreRegistersInputDto input)
        {
            var registration = await _StoreRegistersManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreRegistersOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreRegistersOutputDto>> GetAdvanceSearch(StoreRegistersAdvanceSearchInputDto input)
        {
            try
            {
                var filtereddatatquery = await _StoreRegistersManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.MapTo<List<GetStoreRegistersOutputDto>>();
                mapDataquery.ForEach(x => x.recordsTotal = _StoreRegistersManager.GetRecordCount().Result);
                return new ListResultDto<GetStoreRegistersOutputDto>(mapDataquery);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}