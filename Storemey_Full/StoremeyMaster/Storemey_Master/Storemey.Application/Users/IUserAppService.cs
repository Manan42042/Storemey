using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.Roles.Dto;
using Storemey.Users.Dto;

namespace Storemey.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}