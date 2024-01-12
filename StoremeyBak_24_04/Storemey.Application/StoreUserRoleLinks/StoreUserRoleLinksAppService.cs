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
using Storemey.StoreUserRoleLinks.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreUserRoleLinks
{
    [AbpAuthorize]
    public class StoreUserRoleLinksAppService : AbpServiceBase, IStoreUserRoleLinksAppService
    {
        private readonly IStoreUserRoleLinksManager _StoreUserRoleLinksManager;
        private readonly IRepository<StoreUserRoleLinks, Guid> _StoreUserRoleLinksRepository;

        public StoreUserRoleLinksAppService(
            IStoreUserRoleLinksManager StoreUserRoleLinksManager,
            IRepository<StoreUserRoleLinks, Guid> StoreUserRoleLinksRepository)
        {
            _StoreUserRoleLinksManager = StoreUserRoleLinksManager;
            _StoreUserRoleLinksRepository = StoreUserRoleLinksRepository;
        }

        public async Task<ListResultDto<GetStoreUserRoleLinksOutputDto>> ListAll()
        {
            var events = await _StoreUserRoleLinksManager.ListAll();
            var returnData = events.MapTo<List<GetStoreUserRoleLinksOutputDto>>();
            return new ListResultDto<GetStoreUserRoleLinksOutputDto>(returnData);
        }


        public async Task Create(CreateStoreUserRoleLinksInputDto input)
        {
            var mapData = input.MapTo<StoreUserRoleLinks>();
            await _StoreUserRoleLinksManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreUserRoleLinksInputDto input)
        {
            var mapData = input.MapTo<StoreUserRoleLinks>();
            await _StoreUserRoleLinksManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreUserRoleLinksInputDto input)
        {
            var mapData = input.MapTo<StoreUserRoleLinks>();
            await _StoreUserRoleLinksManager
                .Delete(mapData);
        }


        public async Task<GetStoreUserRoleLinksOutputDto> GetById(GetStoreUserRoleLinksInputDto input)
        {
            var registration = await _StoreUserRoleLinksManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreUserRoleLinksOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreUserRoleLinksOutputDto>> GetAdvanceSearch(StoreUserRoleLinksAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreUserRoleLinksManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.Item1.MapTo<List<GetStoreUserRoleLinksOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = filtereddatatquery.Item2);
            return new ListResultDto<GetStoreUserRoleLinksOutputDto>(mapDataquery);

        }


    }
}