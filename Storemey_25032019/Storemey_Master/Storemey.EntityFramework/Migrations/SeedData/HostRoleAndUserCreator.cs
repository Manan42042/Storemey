using System.Linq;
using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.MultiTenancy;
using Storemey.Authorization;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.EntityFramework;
using Microsoft.AspNet.Identity;

namespace Storemey.Migrations.SeedData
{
    public class HostRoleAndUserCreator
    {
        private readonly StoremeyDbContext _context;

        public HostRoleAndUserCreator(StoremeyDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateHostRoleAndUsers();
        }

        private void CreateHostRoleAndUsers()
        {
            //Admin role for host

            var adminRoleForHost = _context.Roles.FirstOrDefault(r => r.TenantId == null && r.Name == StaticRoleNames.Host.Admin);
            if (adminRoleForHost == null)
            {
                adminRoleForHost = _context.Roles.Add(new Role { Name = StaticRoleNames.Host.Admin, DisplayName = StaticRoleNames.Host.Admin, IsStatic = true });
                _context.SaveChanges();

                //Grant all tenant permissions
                var permissions = PermissionFinder
                    .GetAllPermissions(new StoremeyAuthorizationProvider())
                    .Where(p => p.MultiTenancySides.HasFlag(MultiTenancySides.Host))
                    .ToList();

                foreach (var permission in permissions)
                {
                    _context.Permissions.Add(
                        new RolePermissionSetting
                        {
                            Name = permission.Name,
                            IsGranted = true,
                            RoleId = adminRoleForHost.Id
                        });
                }

                _context.SaveChanges();
            }

            //Admin user for tenancy host

            //var adminUserForHost = _context.Users.FirstOrDefault(u => u.TenantId == null && u.UserName == User.AdminUserName);
            //if (adminUserForHost == null)
            //{
            //    adminUserForHost = _context.Users.Add(
            //        new User
            //        {
            //            UserName = User.AdminUserName,
            //            Name = "System",
            //            Surname = "Administrator",
            //            EmailAddress = "admin@storemey.com",
            //            IsEmailConfirmed = true,
            //            Password = new PasswordHasher().HashPassword( (!string.IsNullOrEmpty(StoremeyConsts.tenantPassword)) ? StoremeyConsts.tenantPassword : User.DefaultPassword),
            //            PasswordResetCode = (!string.IsNullOrEmpty(StoremeyConsts.tenantPassword)) ? StoremeyConsts.tenantPassword : User.DefaultPassword
            //        });

            //    _context.SaveChanges();

            //    _context.UserRoles.Add(new UserRole(null, adminUserForHost.Id, adminRoleForHost.Id));

            //    _context.SaveChanges();
            //}
        }
    }
}