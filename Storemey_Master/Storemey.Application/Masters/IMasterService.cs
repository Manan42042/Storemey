using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreBrands.Dto;
using Storemey.StoreCashRegister.Dto;
using Storemey.StoreCategories.Dto;
using Storemey.StoreCityMaster.Dto;
using Storemey.StoreCountryMaster.Dto;
using Storemey.StoreCurrencies.Dto;
using Storemey.StoreOutlets.Dto;
using Storemey.StoreProductVariants.Dto;
using Storemey.StoreReceiptTemplates.Dto;
using Storemey.StoreRegisters.Dto;
using Storemey.StoreStateMaster.Dto;
using Storemey.StoreTimeZones.Dto;
using Storemey.StoreWarehouses.Dto;

namespace Storemey.MasterService
{

    public interface IMasterService : IApplicationService
    {
        Task<ListResultDto<GetStoreCountryMasterOutputDto>> ListAllCountries();
        Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAllStates(long CountryId);
        Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAllCities(long StateId);
        Task<ListResultDto<GetStoreCurrenciesOutputDto>> ListAllCurrancies();
        Task<ListResultDto<GetStoreTimeZonesOutputDto>> ListAllTimeZones();
        Task<ListResultDto<GetStoreWarehousesOutputDto>> ListAllWarehouses();
        Task<ListResultDto<GetStoreOutletsOutputDto>> ListAlloutlets();
        Task<ListResultDto<GetStoreRegistersOutputDto>> ListAllregister();
        Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> ListAllReceiptTemplates();
        Task<ListResultDto<GetStoreProductVariantsOutputDto>> ListAllVariants();
        Task<ListResultDto<GetStoreCategoriesOutputDto>> ListAllCategories();



    }
}
