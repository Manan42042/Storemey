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
using Storemey.StoreSeasons.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreSeasons
{
    [AbpAuthorize]
    public class StoreSeasonsAppService : AbpServiceBase, IStoreSeasonsAppService
    {
        private readonly IStoreSeasonsManager _StoreSeasonsManager;
        private readonly IRepository<StoreSeasons, Guid> _StoreSeasonsRepository;

        public StoreSeasonsAppService(
            IStoreSeasonsManager StoreSeasonsManager,
            IRepository<StoreSeasons, Guid> StoreSeasonsRepository)
        {
            _StoreSeasonsManager = StoreSeasonsManager;
            _StoreSeasonsRepository = StoreSeasonsRepository;
        }

        public async Task<ListResultDto<GetStoreSeasonsOutputDto>> ListAll()
        {
            var events = await _StoreSeasonsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreSeasonsOutputDto>>();
            return new ListResultDto<GetStoreSeasonsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreSeasonsInputDto input)
        {
            var mapData = input.MapTo<StoreSeasons>();
            await _StoreSeasonsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreSeasonsInputDto input)
        {
            var mapData = input.MapTo<StoreSeasons>();
            await _StoreSeasonsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreSeasonsInputDto input)
        {
            var mapData = input.MapTo<StoreSeasons>();
            await _StoreSeasonsManager
                .Delete(mapData);
        }


        public async Task<GetStoreSeasonsOutputDto> GetById(GetStoreSeasonsInputDto input)
        {
            var registration = await _StoreSeasonsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreSeasonsOutputDto>();

            return mapData;
        }

        [AbpAllowAnonymous]
        public async Task<ListResultDto<GetStoreSeasonsOutputDto>> GetAdvanceSearch(StoreSeasonsAdvanceSearchInputDto input)
        {


            var filtereddatatquery = await _StoreSeasonsManager.ListAllQueryable(input.SearchText, input.CurrentPage, input.MaxRecords, input.SortColumn, input.SortDirection);

            var mapDataquery = filtereddatatquery.ToList().MapTo<List<GetStoreSeasonsOutputDto>>();
            mapDataquery.ForEach(x => x.recordsTotal = _StoreSeasonsManager.GetRecordCount().Result);
            return new ListResultDto<GetStoreSeasonsOutputDto>(mapDataquery);

        }


    }
}