using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreOutlets.Dto;

namespace Storemey.StoreOutlets
{

    public interface IStoreOutletsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreOutletsOutputDto>> ListAll();
        
        Task Create(CreateStoreOutletsInputDto input);

        Task Update(UpdateStoreOutletsInputDto input);

        Task Delete(DeleteStoreOutletsInputDto input);

        Task<GetStoreOutletsOutputDto> GetById(GetStoreOutletsInputDto input);

        Task<ListResultDto<GetStoreOutletsOutputDto>> GetAdvanceSearch(StoreOutletsAdvanceSearchInputDto input);

    }
}
