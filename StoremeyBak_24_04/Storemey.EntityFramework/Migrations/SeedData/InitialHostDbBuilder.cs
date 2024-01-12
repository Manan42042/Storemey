using Storemey.EntityFramework;
using EntityFramework.DynamicFilters;

namespace Storemey.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly StoremeyDbContext _context;

        public InitialHostDbBuilder(StoremeyDbContext context)
        {
            _context = context;
        }

        public void Create(string TenantName)
        {
            _context.DisableAllFilters();

            
            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create(TenantName);
        }
    }
}
