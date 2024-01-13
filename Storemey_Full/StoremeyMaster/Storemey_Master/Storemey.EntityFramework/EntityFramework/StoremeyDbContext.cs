using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.MultiTenancy;

namespace Storemey.EntityFramework
{
    public class StoremeyDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */

        public StoremeyDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in StoremeyDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of StoremeyDbContext since ABP automatically handles it.
         */
        public StoremeyDbContext(string nameOrConnectionString)
            : base(StoremeyConsts.tenantName == "" ? nameOrConnectionString : CommonEntityHelper.TenancyConnectionString(StoremeyConsts.isMaindatabse, StoremeyConsts.tenantName))
        {

        }

        //This constructor is used in tests
        public StoremeyDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public StoremeyDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            base.OnModelCreating(modelBuilder);

        }
    }
}
