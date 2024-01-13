using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.MultiTenancy.Dto;
using System.Threading.Tasks;

namespace Storemey.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
        Task<TenantDto> CreateTeant(CreateTenantDto input);

        Task<TenantDto> AddTeantIntoMasterDatabase(CreateTenantDto input);
        Task AddstoreUserDatabase(CreateTenantDto input);

        Task<bool> IsExpired(string storename);

        Task<bool> TenancyExistsAsync(string TenancyName);
    }
}
