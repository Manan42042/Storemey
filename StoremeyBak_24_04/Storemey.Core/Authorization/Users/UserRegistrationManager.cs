using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Storemey.Authorization.Roles;
using Storemey.MultiTenancy;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace Storemey.Authorization.Users
{
    public class UserRegistrationManager : DomainService
    {
        public IAbpSession AbpSession { get; set; }

        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;

        public UserRegistrationManager(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;

            AbpSession = NullAbpSession.Instance;
        }

        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed)
        {
            CheckForTenant();

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = true,
                Roles = new List<UserRole>()
            };

            user.Password = new PasswordHasher().HashPassword(plainPassword);

            foreach (var defaultRole in _roleManager.Roles.Where(r => r.IsDefault).ToList())
            {
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                throw new InvalidOperationException("Can not register host users!");
            }
        }



        public async Task<long> RegisterAsync2(long id, string name, string surname, string emailAddress, string userName, string plainPassword, bool isEmailConfirmed, bool isDelete = false)
        {
            
            try
            {
                //user.Password = new PasswordHasher().HashPassword(plainPassword);
                var datad = await _userManager.GetByID(id);


                datad.Name = name;
                datad.Surname = surname;
                datad.EmailAddress = emailAddress;
                datad.IsActive = true;
                datad.UserName = userName;
                datad.Password = !string.IsNullOrEmpty(plainPassword) ?  new PasswordHasher().HashPassword(plainPassword) : string.Empty;
                datad.PasswordResetCode = plainPassword;



                if (!isDelete)
                {

                    if (datad.Id > 0)
                    {

                        await _userManager.Update(datad);

                    }
                    else
                    {
                        try
                        {
                            // Your code...
                            // Could also be before try if you know the exception occurs in SaveChanges

                            await _userManager.Create(datad);
                        }
                        catch (DbEntityValidationException e)
                        {
                            foreach (var eve in e.EntityValidationErrors)
                            {
                                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                foreach (var ve in eve.ValidationErrors)
                                {
                                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                        ve.PropertyName, ve.ErrorMessage);
                                }
                            }
                            throw;
                        }

                    }
                }
                else
                {
                    await _userManager.Delete(datad);
                }
                return datad.Id;

            }
            catch (Exception EX)
            {

            }
            return 0;
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));
            }

            return tenant;
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}