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
using Storemey.StoreOutlets.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreOutlets
{
    [AbpAuthorize]
    public class StoreOutletsAppService : AbpServiceBase, IStoreOutletsAppService
    {
        private readonly IStoreOutletsManager _StoreOutletsManager;
        private readonly IRepository<StoreOutlets, Guid> _StoreOutletsRepository;

        public StoreOutletsAppService(
            IStoreOutletsManager StoreOutletsManager,
            IRepository<StoreOutlets, Guid> StoreOutletsRepository)
        {
            _StoreOutletsManager = StoreOutletsManager;
            _StoreOutletsRepository = StoreOutletsRepository;
        }

        public async Task<ListResultDto<GetStoreOutletsOutputDto>> ListAll()
        {
            var events = await _StoreOutletsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreOutletsOutputDto>>();
            return new ListResultDto<GetStoreOutletsOutputDto>(returnData);
        }
        public async Task<List<GetStoreOutletsOutputDto>> ListAlldata()
        {
            var events = await _StoreOutletsManager.ListAlldata();
            var returnData = events.MapTo<List<GetStoreOutletsOutputDto>>();
            return (returnData);
        }


        public async Task Create(CreateStoreOutletsInputDto input)
        {
            var mapData = input.MapTo<StoreOutlets>();
            await _StoreOutletsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreOutletsInputDto input)
        {
            var mapData = input.MapTo<StoreOutlets>();
            await _StoreOutletsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreOutletsInputDto input)
        {
            var mapData = input.MapTo<StoreOutlets>();
            await _StoreOutletsManager
                .Delete(mapData);
        }


        public async Task<GetStoreOutletsOutputDto> GetById(GetStoreOutletsInputDto input)
        {
            try
            {

                var registration = await _StoreOutletsManager.GetByID(input.Id);

                var mapData = registration.MapTo<GetStoreOutletsOutputDto>();

                return mapData;

            }
            catch (Exception)
            {
                return null;
            }
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreOutletsOutputDto>> GetAdvanceSearch(StoreOutletsAdvanceSearchInputDto input)
        {
            try
            {
                var filtereddatatquery = await _StoreOutletsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

                var mapDataquery = filtereddatatquery.MapTo<List<GetStoreOutletsOutputDto>>();
                return new ListResultDto<GetStoreOutletsOutputDto>(mapDataquery);

            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}