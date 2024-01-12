using System.Linq;
using Storemey.EntityFramework;
using Storemey.MultiTenancy;

namespace Storemey.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly StoremeyDbContext _context;

        public DefaultTenantCreator(StoremeyDbContext context)
        {
            _context = context;
        }

        public void Create(string TenenatName)
        {
            CreateUserAndRoles(TenenatName);
        }

        private void CreateUserAndRoles(string TenenatName)
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == (!string.IsNullOrEmpty(TenenatName) ? TenenatName : Tenant.DefaultTenantName));
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
