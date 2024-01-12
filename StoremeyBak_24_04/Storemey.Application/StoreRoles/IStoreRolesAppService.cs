using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreRoles.Dto;

namespace Storemey.StoreRoles
{

    public interface IStoreRolesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreRolesOutputDto>> ListAll();
        
        Task Create(CreateStoreRolesInputDto input);

        Task Update(UpdateStoreRolesInputDto input);

        Task Delete(DeleteStoreRolesInputDto input);

        Task<GetStoreRolesOutputDto> GetById(GetStoreRolesInputDto input);

        Task<ListResultDto<GetStoreRolesOutputDto>> GetAdvanceSearch(StoreRolesAdvanceSearchInputDto input);

    }
}
