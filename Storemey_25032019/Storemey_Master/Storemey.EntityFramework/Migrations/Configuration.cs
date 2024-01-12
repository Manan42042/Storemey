using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Storemey.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace Storemey.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<Storemey.EntityFramework.StoremeyDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Storemey";
        }

        protected override void Seed(Storemey.EntityFramework.StoremeyDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create(Tenant.Name);

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create(Tenant.Name);
                new TenantRoleAndUserBuilder(context, 1).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...

                new InitialHostDbBuilder(context).Create(Tenant.Name);
                
                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create(Tenant.Name);
                new TenantRoleAndUserBuilder(context, 1).Create();
            }

            context.SaveChanges();
        }
    }
}
