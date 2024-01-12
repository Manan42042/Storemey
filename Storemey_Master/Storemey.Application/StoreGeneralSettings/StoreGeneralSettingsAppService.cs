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
using Storemey.StoreGeneralSettings.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;

namespace Storemey.StoreGeneralSettings
{
    [AbpAuthorize]
    public class StoreGeneralSettingsAppService : AbpServiceBase, IStoreGeneralSettingsAppService
    {
        private readonly IStoreGeneralSettingsManager _StoreGeneralSettingsManager;
        private readonly IRepository<StoreGeneralSettings, Guid> _StoreGeneralSettingsRepository;

        public StoreGeneralSettingsAppService(
            IStoreGeneralSettingsManager StoreGeneralSettingsManager,
            IRepository<StoreGeneralSettings, Guid> StoreGeneralSettingsRepository)
        {
            _StoreGeneralSettingsManager = StoreGeneralSettingsManager;
            _StoreGeneralSettingsRepository = StoreGeneralSettingsRepository;
        }

        public async Task<ListResultDto<GetStoreGeneralSettingsOutputDto>> ListAll()
        {
            var events = await _StoreGeneralSettingsManager.ListAll();
            var returnData = events.MapTo<List<GetStoreGeneralSettingsOutputDto>>();
            return new ListResultDto<GetStoreGeneralSettingsOutputDto>(returnData);
        }


        public async Task Create(CreateStoreGeneralSettingsInputDto input)
        {
            var mapData = input.MapTo<StoreGeneralSettings>();
            await _StoreGeneralSettingsManager
                .Create(mapData);
        }

        public async Task Update(UpdateStoreGeneralSettingsInputDto input)
        {
            var mapData = input.MapTo<StoreGeneralSettings>();
            await _StoreGeneralSettingsManager
                .Update(mapData);
        }

        public async Task Delete(DeleteStoreGeneralSettingsInputDto input)
        {
            var mapData = input.MapTo<StoreGeneralSettings>();
            await _StoreGeneralSettingsManager
                .Delete(mapData);
        }


        public async Task<GetStoreGeneralSettingsOutputDto> GetById(GetStoreGeneralSettingsInputDto input)
        {
            var registration = await _StoreGeneralSettingsManager.GetByID(input.Id);

            var mapData = registration.MapTo<GetStoreGeneralSettingsOutputDto>();

            return mapData;
        }

       
    }
}