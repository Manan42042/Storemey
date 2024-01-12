using System.Data.Common;
using System.Data.Entity;
using Abp.Domain.Repositories;
using Abp.EntityFramework;
using Abp.Zero.EntityFramework;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.EntityFramework.Repositories;
using Storemey.MultiTenancy;

namespace Storemey.EntityFramework
{
    [AutoRepositoryTypes(
    typeof(IRepository<>),
    typeof(IRepository<,>),
    typeof(SimpleTaskSystemRepositoryBase<>),
    typeof(SimpleTaskSystemRepositoryBase<,>)
)]
    public class SimpleTaskSystemDbContext : AbpDbContext
    {

        // MASTER ADMIN ZONE
        public virtual IDbSet<Storemey.MasterCountries.MasterCountries> MasterCountries { get; set; }
        public virtual IDbSet<Storemey.MasterPlans.MasterPlans> MasterPlans { get; set; }
        public virtual IDbSet<Storemey.MasterPlanServices.MasterPlanServices> MasterPlanServices { get; set; }
        public virtual IDbSet<Storemey.MasterPlanPrices.MasterPlanPrices> MasterPlanPrices { get; set; }
        public virtual IDbSet<Storemey.AdminStores.AdminStores> AdminStores { get; set; }

        public virtual IDbSet<Storemey.AdminBugTrackerComments.AdminBugTrackerComments> AdminBugTrackerComments { get; set; }
        public virtual IDbSet<Storemey.AdminBugTrackers.AdminBugTrackers> AdminBugTrackers { get; set; }
        public virtual IDbSet<Storemey.AdminEmailTemplates.AdminEmailTemplates> AdminEmailTemplates { get; set; }
        public virtual IDbSet<Storemey.AdminSMTPsettings.AdminSMTPsettings> AdminSMTPsettings { get; set; }
        public virtual IDbSet<Storemey.AdminStoreScheduler.AdminStoreScheduler> AdminStoreScheduler { get; set; }
        public virtual IDbSet<Storemey.AdminUpdateAllDatabase.AdminUpdateAllDatabase> AdminUpdateAllDatabase { get; set; }


        // USER ZONE

        public virtual IDbSet<Storemey.StoreCurrencies.StoreCurrencies> StoreCurrencies { get; set; }
        public virtual IDbSet<Storemey.StoreGeneralSettings.StoreGeneralSettings> StoreGeneralSettings { get; set; }
        public virtual IDbSet<Storemey.StoreUsers.StoreUsers> StoreUsers { get; set; }

        public virtual IDbSet<Storemey.StoreTaxGroups.StoreTaxGroups> StoreTaxGroups { get; set; } //1
        public virtual IDbSet<Storemey.StoreTaxGroupMapping.StoreTaxGroupMapping> StoreTaxGroupMapping { get; set; } //2
        public virtual IDbSet<Storemey.StoreTax.StoreTax> StoreTax { get; set; }//3

        public virtual IDbSet<Storemey.StoreTags.StoreTags> StoreTags { get; set; } //4
        public virtual IDbSet<Storemey.StoreSuppliers.StoreSuppliers> StoreSuppliers { get; set; } // 5
        public virtual IDbSet<Storemey.StoreStateMaster.StoreStateMaster> StoreStateMaster { get; set; } // 6
        public virtual IDbSet<Storemey.StoreReceiptTemplates.StoreReceiptTemplates> StoreReceiptTemplates { get; set; } //7
        public virtual IDbSet<Storemey.StorePaymentTypes.StorePaymentTypes> StorePaymentTypes { get; set; } //8
        public virtual IDbSet<Storemey.StoreCustomers.StoreCustomers> StoreCustomers { get; set; } //9
        public virtual IDbSet<Storemey.StoreCountryMaster.StoreCountryMaster> StoreCountryMaster { get; set; } //11
        public virtual IDbSet<Storemey.StoreCityMaster.StoreCityMaster> StoreCityMaster { get; set; } // 12
        public virtual IDbSet<Storemey.StoreCategories.StoreCategories> StoreCategories { get; set; } // 13 
        public virtual IDbSet<Storemey.StoreBrands.StoreBrands> StoreBrands { get; set; } //14
        public virtual IDbSet<Storemey.StoreTimeZones.StoreTimeZones> StoreTimeZones { get; set; } //14
        public virtual IDbSet<Storemey.StoreWarehouses.StoreWarehouses> StoreWarehouses { get; set; } //14
        public virtual IDbSet<Storemey.StoreOutlets.StoreOutlets> StoreOutlets { get; set; } //14
        public virtual IDbSet<Storemey.StoreRegisters.StoreRegisters> StoreRegisters { get; set; } //14
        public virtual IDbSet<Storemey.StoreSeasons.StoreSeasons> StoreSeasons { get; set; } //14
        public virtual IDbSet<Storemey.StoreRoles.StoreRoles> StoreRoles { get; set; } //14
        public virtual IDbSet<Storemey.StoreUserRoleLinks.StoreUserRoleLinks> StoreUserRoleLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreGiftCards.StoreGiftCards> StoreGiftCards { get; set; } //14
        public virtual IDbSet<Storemey.StoreProducts.StoreProducts> StoreProducts { get; set; } //14
        public virtual IDbSet<Storemey.StoreProductImages.StoreProductImages> StoreStoreProductImagesSeasons { get; set; } //14
        public virtual IDbSet<Storemey.StoreProductBrandLinks.StoreProductBrandLinks> StoreProductBrandLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreProductTagLinks.StoreProductTagLinks> StoreProductTagLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreProductSeasonLinks.StoreProductSeasonLinks> StoreProductSeasonLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreProductCategoryLinks.StoreProductCategoryLinks> StoreProductCategoryLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreInventory.StoreInventory> StoreInventory { get; set; } //14
        public virtual IDbSet<Storemey.StoreInventoryPurchaseOrders.StoreInventoryPurchaseOrders> StoreInventoryPurchaseOrders { get; set; } //14
        public virtual IDbSet<Storemey.StoreInventoryPurchaseLinks.StoreInventoryPurchaseLinks> StoreInventoryPurchaseLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreInventoryTransferOrders.StoreInventoryTransferOrders> StoreInventoryTransferOrders { get; set; } //14
        public virtual IDbSet<Storemey.StoreInventoryTransferLinks.StoreInventoryTransferLinks> StoreInventoryTransferLinks { get; set; } //14
        public virtual IDbSet<Storemey.StoreCashRegister.StoreCashRegister> StoreCashRegister { get; set; } //14
        public virtual IDbSet<Storemey.StoreCashRegisterInOut.StoreCashRegisterInOut> StoreCashRegisterInOut { get; set; } //14
        public virtual IDbSet<Storemey.StoreSaleTransactions.StoreSaleTransactions> StoreSaleTransactions { get; set; } //14
        public virtual IDbSet<Storemey.StoreSaleItems.StoreSaleItems> StoreSaleItems { get; set; } //14
        public virtual IDbSet<Storemey.StoreSalePayments.StoreSalePayments> StoreSalePayments { get; set; } //14





        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */

        public SimpleTaskSystemDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in StoremeyDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of SimpleTaskSystemDbContext  since ABP automatically handles it.
         */
        public SimpleTaskSystemDbContext(string nameOrConnectionString)
            : base(StoremeyConsts.tenantName == "" ? nameOrConnectionString : CommonEntityHelper.TenancyConnectionString(StoremeyConsts.tenantName))
        {

        }

        //This constructor is used in tests
        public SimpleTaskSystemDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public SimpleTaskSystemDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Masters Admin Zone 

            modelBuilder.Entity<Storemey.MasterPlans.MasterPlans>().ToTable("MasterPlans");
            modelBuilder.Entity<Storemey.MasterPlanServices.MasterPlanServices>().ToTable("MasterPlanServices");
            modelBuilder.Entity<Storemey.MasterCountries.MasterCountries>().ToTable("MasterCountries");
            modelBuilder.Entity<Storemey.MasterPlanPrices.MasterPlanPrices>().ToTable("MasterPlanPrices");
            modelBuilder.Entity<Storemey.AdminStores.AdminStores>().ToTable("AdminStores");
            modelBuilder.Entity<Storemey.AdminBugTrackerComments.AdminBugTrackerComments>().ToTable("AdminBugTrackerComments");
            modelBuilder.Entity<Storemey.AdminBugTrackers.AdminBugTrackers>().ToTable("AdminBugTrackers");
            modelBuilder.Entity<Storemey.AdminEmailTemplates.AdminEmailTemplates>().ToTable("AdminEmailTemplates");
            modelBuilder.Entity<Storemey.AdminSMTPsettings.AdminSMTPsettings>().ToTable("AdminSMTPsettings");
            modelBuilder.Entity<Storemey.AdminStoreScheduler.AdminStoreScheduler>().ToTable("AdminStoreScheduler");
            modelBuilder.Entity<Storemey.AdminUpdateAllDatabase.AdminUpdateAllDatabase>().ToTable("AdminUpdateAllDatabase");


            // User Side
            modelBuilder.Entity<Storemey.StoreCurrencies.StoreCurrencies>().ToTable("StoreCurrencies");
            modelBuilder.Entity<Storemey.StoreGeneralSettings.StoreGeneralSettings>().ToTable("StoreGeneralSettings");
            modelBuilder.Entity<Storemey.StoreUsers.StoreUsers>().ToTable("StoreUsers");
            modelBuilder.Entity<Storemey.StoreTaxGroups.StoreTaxGroups>().ToTable("StoreTaxGroups");
            modelBuilder.Entity<Storemey.StoreTaxGroupMapping.StoreTaxGroupMapping>().ToTable("StoreTaxGroupMapping");
            modelBuilder.Entity<Storemey.StoreTax.StoreTax>().ToTable("StoreTax");
            modelBuilder.Entity<Storemey.StoreTags.StoreTags>().ToTable("StoreTags");
            modelBuilder.Entity<Storemey.StoreSuppliers.StoreSuppliers>().ToTable("StoreSuppliers");
            modelBuilder.Entity<Storemey.StoreStateMaster.StoreStateMaster>().ToTable("StoreStateMaster");
            modelBuilder.Entity<Storemey.StoreReceiptTemplates.StoreReceiptTemplates>().ToTable("StoreReceiptTemplates");
            modelBuilder.Entity<Storemey.StorePaymentTypes.StorePaymentTypes>().ToTable("StorePaymentTypes");
            modelBuilder.Entity<Storemey.StoreCustomers.StoreCustomers>().ToTable("StoreCustomers");
            modelBuilder.Entity<Storemey.StoreCountryMaster.StoreCountryMaster>().ToTable("StoreCountryMaster");
            modelBuilder.Entity<Storemey.StoreCityMaster.StoreCityMaster>().ToTable("StoreCityMaster");
            modelBuilder.Entity<Storemey.StoreCategories.StoreCategories>().ToTable("StoreCategories");
            modelBuilder.Entity<Storemey.StoreBrands.StoreBrands>().ToTable("StoreBrands");
            modelBuilder.Entity<Storemey.StoreTimeZones.StoreTimeZones>().ToTable("StoreTimeZones");
            modelBuilder.Entity<Storemey.StoreWarehouses.StoreWarehouses>().ToTable("StoreWarehouses");
            modelBuilder.Entity<Storemey.StoreOutlets.StoreOutlets>().ToTable("StoreOutlets");
            modelBuilder.Entity<Storemey.StoreRegisters.StoreRegisters>().ToTable("StoreRegisters");
            modelBuilder.Entity<Storemey.StoreSeasons.StoreSeasons>().ToTable("StoreSeasons");

            modelBuilder.Entity<Storemey.StoreRoles.StoreRoles>().ToTable("StoreRoles");
            modelBuilder.Entity<Storemey.StoreUserRoleLinks.StoreUserRoleLinks>().ToTable("StoreUserRoleLinks");
            modelBuilder.Entity<Storemey.StoreGiftCards.StoreGiftCards>().ToTable("StoreGiftCards");
            modelBuilder.Entity<Storemey.StoreProducts.StoreProducts>().ToTable("StoreProducts");
            modelBuilder.Entity<Storemey.StoreProductImages.StoreProductImages>().ToTable("StoreProductImages");
            modelBuilder.Entity<Storemey.StoreProductBrandLinks.StoreProductBrandLinks>().ToTable("StoreProductBrandLinks");
            modelBuilder.Entity<Storemey.StoreProductTagLinks.StoreProductTagLinks>().ToTable("StoreProductTagLinks");
            modelBuilder.Entity<Storemey.StoreProductSeasonLinks.StoreProductSeasonLinks>().ToTable("StoreProductSeasonLinks");
            modelBuilder.Entity<Storemey.StoreProductCategoryLinks.StoreProductCategoryLinks>().ToTable("StoreProductCategoryLinks");
            modelBuilder.Entity<Storemey.StoreInventory.StoreInventory>().ToTable("StoreInventory");
            modelBuilder.Entity<Storemey.StoreInventoryPurchaseOrders.StoreInventoryPurchaseOrders>().ToTable("StoreInventoryPurchaseOrders");
            modelBuilder.Entity<Storemey.StoreInventoryPurchaseLinks.StoreInventoryPurchaseLinks>().ToTable("StoreInventoryPurchaseLinks");
            modelBuilder.Entity<Storemey.StoreInventoryTransferOrders.StoreInventoryTransferOrders>().ToTable("StoreInventoryTransferOrders");
            modelBuilder.Entity<Storemey.StoreInventoryTransferLinks.StoreInventoryTransferLinks>().ToTable("StoreInventoryTransferLinks");
            modelBuilder.Entity<Storemey.StoreCashRegister.StoreCashRegister>().ToTable("StoreCashRegister");
            modelBuilder.Entity<Storemey.StoreCashRegisterInOut.StoreCashRegisterInOut>().ToTable("StoreCashRegisterInOut");
            modelBuilder.Entity<Storemey.StoreSaleTransactions.StoreSaleTransactions>().ToTable("StoreSaleTransactions");
            modelBuilder.Entity<Storemey.StoreSaleItems.StoreSaleItems>().ToTable("StoreSaleItems");
            modelBuilder.Entity<Storemey.StoreSalePayments.StoreSalePayments>().ToTable("StoreSalePayments");



            base.OnModelCreating(modelBuilder);


        }
    }
}
