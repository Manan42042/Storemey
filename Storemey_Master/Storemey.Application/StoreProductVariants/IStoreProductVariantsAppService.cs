using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductVariants.Dto;

namespace Storemey.StoreProductVariants
{

    public interface IStoreProductVariantsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductVariantsOutputDto>> ListAll();
        
        Task Create(CreateStoreProductVariantsInputDto input);

        Task Update(UpdateStoreProductVariantsInputDto input);

        Task Delete(DeleteStoreProductVariantsInputDto input);

        Task<GetStoreProductVariantsOutputDto> GetById(GetStoreProductVariantsInputDto input);

        Task<ListResultDto<GetStoreProductVariantsOutputDto>> GetAdvanceSearch(StoreProductVariantsAdvanceSearchInputDto input);

    }
}
