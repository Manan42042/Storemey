using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreUserRoleLinks.Dto;

namespace Storemey.StoreUserRoleLinks
{

    public interface IStoreUserRoleLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreUserRoleLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreUserRoleLinksInputDto input);

        Task Update(UpdateStoreUserRoleLinksInputDto input);

        Task Delete(DeleteStoreUserRoleLinksInputDto input);

        Task<GetStoreUserRoleLinksOutputDto> GetById(GetStoreUserRoleLinksInputDto input);

        Task<ListResultDto<GetStoreUserRoleLinksOutputDto>> GetAdvanceSearch(StoreUserRoleLinksAdvanceSearchInputDto input);

    }
}
