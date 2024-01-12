using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductVariantValues.Dto;

namespace Storemey.StoreProductVariantValues
{

    public interface IStoreProductVariantValuesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductVariantValuesOutputDto>> ListAll();
        
        Task Create(CreateStoreProductVariantValuesInputDto input);

        Task Update(UpdateStoreProductVariantValuesInputDto input);

        Task Delete(DeleteStoreProductVariantValuesInputDto input);

        Task<GetStoreProductVariantValuesOutputDto> GetById(GetStoreProductVariantValuesInputDto input);

        Task<ListResultDto<GetStoreProductVariantValuesOutputDto>> GetAdvanceSearch(StoreProductVariantValuesAdvanceSearchInputDto input);

    }
}
