using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using Storemey.EntityFramework;

namespace Storemey.Migrator
{
    [DependsOn(typeof(StoremeyDataModule))]
    public class StoremeyMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<StoremeyDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}