using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Storemey.Authorization;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.EntityFramework;

namespace Storemey.Migrations.SeedData
{
    public class TenantRoleAndUserBuilder
    {
        private readonly StoremeyDbContext _context;
        private readonly int _tenantId;

        public TenantRoleAndUserBuilder(StoremeyDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateRolesAndUsersAsync();
        }

        private async System.Threading.Tasks.Task CreateRolesAndUsersAsync()
        {
            //Admin role

            var adminRole = _context.Roles.FirstOrDefault(r => r.TenantId == _tenantId && r.Name == StaticRoleNames.Tenants.Admin);
            if (adminRole == null)
            {
                adminRole = _context.Roles.Add(new Role(_tenantId, StaticRoleNames.Tenants.Admin, StaticRoleNames.Tenants.Admin) { IsStatic = true });
                _context.SaveChanges();

                //Grant all permissions to admin role
                var permissions = PermissionFinder
                    .GetAllPermissions(new StoremeyAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Tenant))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            TenantId = _tenantId,
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRole.Id
                        });
                }

                _context.SaveChanges();
            }

            //admin user

            var adminUser = _context.Users.FirstOrDefault(u => u.TenantId == _tenantId && u.UserName == User.AdminUserName);
            if (adminUser == null)
            {
                if (!string.IsNullOrEmpty(StoremeyConsts.tenantPassword))
                {
                    string EmailAddress = "";
                    if (!string.IsNullOrEmpty(StoremeyConsts.tenantUserName))
                    {
                        EmailAddress = StoremeyConsts.tenanEmail;
                    }
                    else
                    {
                        EmailAddress = "User@storemey.com";
                    }

                    //123qwe

                    adminUser = User.CreateTenantAdminUser(_tenantId, EmailAddress, (!string.IsNullOrEmpty(StoremeyConsts.tenantPassword)) ? StoremeyConsts.tenantPassword : User.DefaultPassword);
                    adminUser.UserName = string.IsNullOrEmpty(StoremeyConsts.tenantUserName) ? adminUser.UserName : StoremeyConsts.tenantUserName;
                    adminUser.Name = string.IsNullOrEmpty(StoremeyConsts.tenantUserName) ? adminUser.Name : StoremeyConsts.tenantUserName;

                    adminUser.IsEmailConfirmed = true;
                    adminUser.IsActive = true;
                    adminUser.PasswordResetCode = (!string.IsNullOrEmpty(StoremeyConsts.tenantPassword)) ? StoremeyConsts.tenantPassword : User.DefaultPassword;

                    adminUser.TenantId = null;

                    _context.Users.Add(adminUser);
                    _context.SaveChanges();

                    //Assign Admin role to admin user
                    _context.UserRoles.Add(new UserRole(_tenantId, adminUser.Id, adminRole.Id));
                    _context.SaveChanges();

                }
            }



        }
    }
}