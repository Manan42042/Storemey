using Abp.Authorization;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;

namespace Storemey.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
