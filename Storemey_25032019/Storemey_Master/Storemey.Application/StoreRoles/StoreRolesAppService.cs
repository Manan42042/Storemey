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
using Storemey.StoreRoles.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreRoles
{
    [AbpAuthorize]
    public class StoreRolesAppService : AbpServiceBase, IStoreRolesAppService
    {
        private readonly IStoreRolesManager _StoreRolesManager;
        private readonly IRepository<StoreRoles, Guid> _StoreRolesRepository;

        public StoreRolesAppService(
            IStoreRolesManager StoreRolesManager,
            IRepository<StoreRoles, Guid> StoreRolesRepository)
        {
            _StoreRolesManager = StoreRolesManager;
            _StoreRolesRepository = StoreRolesRepository;
        }

        public async Task<ListResultDto<GetStoreRolesOutputDto>> ListAll()
        {
            var events = await _StoreRolesManager.ListAll();
            var returnData = events.MapTo<List<GetStoreRolesOutputDto>>();
            return new ListResultDto<GetStoreRolesOutputDto>(returnData);
        }


        public async Task Create(CreateStoreRolesInputDto input)
        {
            var mapData = input.MapTo<StoreRoles>();
            await _StoreRolesManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreRolesInputDto input)
        {
            var mapData = input.MapTo<StoreRoles>();
            await _StoreRolesManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreRolesInputDto input)
        {
            var mapData = input.MapTo<StoreRoles>();
            await _StoreRolesManager
                .Delete(mapData);
        }


        public async Task<GetStoreRolesOutputDto> GetById(GetStoreRolesInputDto input)
        {
            var registration = await _StoreRolesManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreRolesOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreRolesOutputDto>> GetAdvanceSearch(StoreRolesAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreRolesManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreRolesOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreRolesManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreRolesOutputDto>(mapDataquery);

        }


    }
}