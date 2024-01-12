using System.Reflection;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Modules;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.MasterCountries.Dto;
using Storemey.MasterPlanPrices.Dto;
using Storemey.MasterPlans.Dto;
using Storemey.MasterPlanServices.Dto;
using Storemey.Roles.Dto;
using Storemey.StoreUsers.Dto;
using Storemey.StoreCurrencies.Dto;
using Storemey.Users.Dto;
using Storemey.StoreTaxGroups.Dto;
using Storemey.StoreTaxGroupMapping.Dto;
using Storemey.StoreTax.Dto;
using Storemey.StoreTags.Dto;
using Storemey.StoreSuppliers.Dto;
using Storemey.StoreStateMaster.Dto;
using Storemey.StoreReceiptTemplates.Dto;
using Storemey.StorePaymentTypes.Dto;
using Storemey.StoreCustomers.Dto;
using Storemey.StoreCountryMaster.Dto;
using Storemey.StoreCityMaster.Dto;
using Storemey.StoreCategories.Dto;
using Storemey.StoreBrands.Dto;
using Storemey.AdminBugTrackers.Dto;
using Storemey.AdminBugTrackerComments.Dto;
using Storemey.AdminEmailTemplates.Dto;
using Storemey.AdminSMTPsettings.Dto;
using Storemey.AdminStores.Dto;
using Storemey.AdminStoreScheduler.Dto;
using Storemey.AdminUpdateAllDatabase.Dto;
using Storemey.StoreTimeZones.Dto;
using Storemey.StoreWarehouses.Dto;
using Storemey.StoreOutlets.Dto;
using Storemey.StoreRegisters.Dto;
using Storemey.StoreSeasons.Dto;
using Storemey.StoreRoles.Dto;
using Storemey.StoreGiftCards.Dto;
using Storemey.StoreUserRoleLinks.Dto;
using Storemey.StoreProductImages.Dto;
using Storemey.StoreProductBrandLinks.Dto;
using Storemey.StoreProductTagLinks.Dto;
using Storemey.StoreProducts.Dto;
using Storemey.StoreProductCategoryLinks.Dto;
using Storemey.StoreProductSeasonLinks.Dto;
using Storemey.StoreInventory.Dto;
using Storemey.StoreInventoryPurchaseOrders.Dto;
using Storemey.StoreInventoryPurchaseLinks.Dto;
using Storemey.StoreInventoryTransferOrders.Dto;
using Storemey.StoreInventoryTransferLinks.Dto;
using Storemey.StoreCashRegister.Dto;
using Storemey.StoreCashRegisterInOut.Dto;
using Storemey.StoreSaleTransactions.Dto;
using Storemey.StoreSaleItems.Dto;
using Storemey.StoreSalePayments.Dto;

namespace Storemey
{
    [DependsOn(typeof(StoremeyCoreModule), typeof(AbpAutoMapperModule))]
    public class StoremeyApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            // TODO: Is there somewhere else to store these, with the dto classes
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                // Role and permission
                cfg.CreateMap<Permission, string>().ConvertUsing(r => r.Name);
                cfg.CreateMap<RolePermissionSetting, string>().ConvertUsing(r => r.Name);

                cfg.CreateMap<CreateRoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());
                cfg.CreateMap<RoleDto, Role>().ForMember(x => x.Permissions, opt => opt.Ignore());

                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<UserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());

                cfg.CreateMap<CreateUserDto, User>();
                cfg.CreateMap<CreateUserDto, User>().ForMember(x => x.Roles, opt => opt.Ignore());


                cfg.CreateMap<GetMasterPlansOutputDto, Storemey.MasterPlans.MasterPlans>();
                cfg.CreateMap<GetMasterPlanServicesOutputDto, Storemey.MasterPlans.MasterPlans>();

                cfg.CreateMap<Storemey.MasterPlanPrices.MasterPlanPrices, GetMasterPlanPricesOutputDto>();
                cfg.CreateMap<GetMasterPlanPricesOutputDto, Storemey.MasterPlanPrices.MasterPlanPrices>();

                cfg.CreateMap<Storemey.MasterPlans.MasterPlans, GetMasterPlanServicesOutputDto>();
                cfg.CreateMap<Storemey.MasterPlans.MasterPlans, GetMasterPlansOutputDto>()
                .ForMember(x => x.PlanServices, opt => opt.Ignore())
                .ForMember(x => x.Price, opt => opt.Ignore());

       



                cfg.CreateMap<Storemey.MasterPlanServices.MasterPlanServices, GetMasterPlanServicesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.MasterCountries.MasterCountries, GetMasterCountriesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreUsers.StoreUsers, GetStoreUsersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreUsers.StoreUsers, GetStoreUsersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCurrencies.StoreCurrencies, GetStoreCurrenciesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreTaxGroups.StoreTaxGroups, GetStoreTaxGroupsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreTaxGroupMapping.StoreTaxGroupMapping, GetStoreTaxGroupMappingOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreTax.StoreTax, GetStoreTaxOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreTags.StoreTags, GetStoreTagsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreSuppliers.StoreSuppliers, GetStoreSuppliersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreStateMaster.StoreStateMaster, GetStoreStateMasterOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreReceiptTemplates.StoreReceiptTemplates, GetStoreReceiptTemplatesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StorePaymentTypes.StorePaymentTypes, GetStorePaymentTypesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCustomers.StoreCustomers, GetStoreCustomersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCountryMaster.StoreCountryMaster, GetStoreCountryMasterOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCityMaster.StoreCityMaster, GetStoreCityMasterOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCategories.StoreCategories, GetStoreCategoriesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreBrands.StoreBrands, GetStoreBrandsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreTimeZones.StoreTimeZones, GetStoreTimeZonesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());



                cfg.CreateMap<Storemey.AdminBugTrackers.AdminBugTrackers, GetAdminBugTrackersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminBugTrackerComments.AdminBugTrackerComments, GetAdminBugTrackerCommentsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminEmailTemplates.AdminEmailTemplates, GetAdminEmailTemplatesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminSMTPsettings.AdminSMTPsettings, GetAdminSMTPsettingsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminStores.AdminStores, GetAdminStoresOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminStoreScheduler.AdminStoreScheduler, GetAdminStoreSchedulerOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.AdminUpdateAllDatabase.AdminUpdateAllDatabase, GetAdminUpdateAllDatabaseOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreWarehouses.StoreWarehouses, GetStoreWarehousesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreOutlets.StoreOutlets, GetStoreOutletsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreRegisters.StoreRegisters, GetStoreRegistersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreSeasons.StoreSeasons, GetStoreSeasonsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreRoles.StoreRoles, GetStoreRolesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreUserRoleLinks.StoreUserRoleLinks, GetStoreUserRoleLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreGiftCards.StoreGiftCards, GetStoreGiftCardsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProducts.StoreProducts, GetStoreProductsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProductImages.StoreProductImages, GetStoreProductImagesOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProductBrandLinks.StoreProductBrandLinks, GetStoreProductBrandLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProductTagLinks.StoreProductTagLinks, GetStoreProductTagLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProductSeasonLinks.StoreProductSeasonLinks, GetStoreProductSeasonLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreProductCategoryLinks.StoreProductCategoryLinks, GetStoreProductCategoryLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreInventory.StoreInventory, GetStoreInventoryOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreInventoryPurchaseOrders.StoreInventoryPurchaseOrders, GetStoreInventoryPurchaseOrdersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreInventoryPurchaseLinks.StoreInventoryPurchaseLinks, GetStoreInventoryPurchaseLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreInventoryTransferOrders.StoreInventoryTransferOrders, GetStoreInventoryTransferOrdersOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreInventoryTransferLinks.StoreInventoryTransferLinks, GetStoreInventoryTransferLinksOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCashRegister.StoreCashRegister, GetStoreCashRegisterOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreCashRegisterInOut.StoreCashRegisterInOut, GetStoreCashRegisterInOutOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreSaleTransactions.StoreSaleTransactions, GetStoreSaleTransactionsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreSaleItems.StoreSaleItems, GetStoreSaleItemsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                cfg.CreateMap<Storemey.StoreSalePayments.StoreSalePayments, GetStoreSalePaymentsOutputDto>().ForMember(x => x.recordsTotal, opt => opt.Ignore());
                
            });
        }
    }
}
