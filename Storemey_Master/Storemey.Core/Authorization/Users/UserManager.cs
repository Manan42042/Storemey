using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Storemey.Authorization.Roles;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace Storemey.Authorization.Users
{
    public class UserManager : AbpUserManager<Role, User>, IUserManager
    {


        private readonly IRepository<User, long> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IRepository<User, long> myMasterRepository,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                  userStore,
                  roleManager,
                  permissionManager,
                  unitOfWorkManager,
                  cacheManager,
                  organizationUnitRepository,
                  userOrganizationUnitRepository,
                  organizationUnitSettings,
                  localizationManager,
                  emailService,
                  settingManager,
                  userTokenProviderAccessor)
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        public async Task Create(User input)
        {
            await _thisRpository.InsertAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Update(User input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(User input)
        {
            try
            {
                await _thisRpository.DeleteAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                    }
                }
                throw;
            }

        }

        public async Task<User> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.Id == ID);

            if (@MasterCountries == null)
            {
                return new User();
            }
            return @MasterCountries;
        }
    }
}