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
using Storemey.StoreBrands.Dto;
using FileHelpers;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using Abp;
using Storemey.StoreCountryMaster;
using Storemey.StoreCountryMaster.Dto;
using Storemey.StoreStateMaster;
using Storemey.StoreCityMaster;
using Storemey.StoreCurrencies;
using Storemey.StoreTimeZones;
using Storemey.StoreStateMaster.Dto;
using Storemey.StoreCityMaster.Dto;
using Storemey.StoreCurrencies.Dto;
using Storemey.StoreTimeZones.Dto;
using Storemey.StoreWarehouses.Dto;
using Storemey.StoreWarehouses;
using Storemey.StoreOutlets;
using Storemey.StoreReceiptTemplates;
using Storemey.StoreOutlets.Dto;
using Storemey.StoreReceiptTemplates.Dto;

namespace Storemey.MasterService
{
    [AbpAuthorize]
    public class MasterService : AbpServiceBase, IMasterService
    {
        private readonly IStoreCountryMasterAppService _StoreCountryMasterAppService;
        private readonly IStoreStateMasterAppService _StoreStateMasterAppService;
        private readonly IStoreCityMasterAppService _StoreCityMasterAppService;
        private readonly IStoreCurrenciesAppService _StoreCurrenciesAppService;
        private readonly IStoreTimeZonesAppService _StoreTimeZonesAppService;
        private readonly IStoreWarehousesAppService _StoreWarehousesAppService;
        private readonly IStoreOutletsAppService _StoreOutletsAppService;
        private readonly IStoreReceiptTemplatesAppService _StoreReceiptTemplatesAppService;

        public static int maxRecords = 20;



        public MasterService(
            IStoreCountryMasterAppService StoreCountryMasterAppService,
            IStoreStateMasterAppService StoreStateMasterAppService,
            IStoreCityMasterAppService StoreCityMasterAppService,
            IStoreCurrenciesAppService StoreCurrenciesAppService,
            IStoreWarehousesAppService StoreWarehousesAppService,
            IStoreOutletsAppService StoreOutletsAppService,
            IStoreReceiptTemplatesAppService StoreReceiptTemplatesAppService,
            IStoreTimeZonesAppService StoreTimeZonesAppService)
        {
            _StoreCountryMasterAppService = StoreCountryMasterAppService;
            _StoreStateMasterAppService = StoreStateMasterAppService;
            _StoreCityMasterAppService = StoreCityMasterAppService;
            _StoreCurrenciesAppService = StoreCurrenciesAppService;
            _StoreTimeZonesAppService = StoreTimeZonesAppService;
            _StoreWarehousesAppService = StoreWarehousesAppService;

            _StoreOutletsAppService = StoreOutletsAppService;
            _StoreReceiptTemplatesAppService = StoreReceiptTemplatesAppService;

        }

        public async Task<ListResultDto<GetStoreCountryMasterOutputDto>> ListAllCountries()
        {
            var events = await _StoreCountryMasterAppService.ListAll();
            return events;
        }

        public async Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAllStates(long CountryId)
        {
            var events = await _StoreStateMasterAppService.ListAllByCountryID(CountryId);
            return events;
        }

        public async Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAllCities(long StateId)
        {
            var events = await _StoreCityMasterAppService.ListAllByStateId(StateId);
            return events;
        }

        public async Task<ListResultDto<GetStoreCurrenciesOutputDto>> ListAllCurrancies()
        {

            try
            {

                var events = await _StoreCurrenciesAppService.ListAll();
                return events;

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ListResultDto<GetStoreTimeZonesOutputDto>> ListAllTimeZones()
        {
            try
            {
                var events = await _StoreTimeZonesAppService.ListAll();
            return events;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task<ListResultDto<GetStoreWarehousesOutputDto>> ListAllWarehouses()
        {
            try
            {
                var events = await _StoreWarehousesAppService.ListAll();
                return events;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ListResultDto<GetStoreOutletsOutputDto>> ListAlloutlets()
        {
            try
            {
                var events = await _StoreOutletsAppService.ListAll();
                return events;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> ListAllReceiptTemplates()
        {
            try
            {
                var events = await _StoreReceiptTemplatesAppService.ListAll();
                return events;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


    }
}