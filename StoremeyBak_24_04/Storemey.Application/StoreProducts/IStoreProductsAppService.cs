using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProducts.Dto;

namespace Storemey.StoreProducts
{

    public interface IStoreProductsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductsOutputDto>> ListAll();
        
        Task Create(CreateStoreProductsInputDto input);

        Task Update(UpdateStoreProductsInputDto input);

        Task Delete(DeleteStoreProductsInputDto input);

        Task<GetStoreProductsOutputDto> GetById(GetStoreProductsInputDto input);

        Task<ListResultDto<GetStoreProductsOutputDto>> GetAdvanceSearch(StoreProductsAdvanceSearchInputDto input);

    }
}
