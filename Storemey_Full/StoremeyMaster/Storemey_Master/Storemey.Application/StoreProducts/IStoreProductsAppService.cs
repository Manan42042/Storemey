using System;
using System.Collections.Generic;
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

        Task DeleteByProductId(long ProductId);

        Task<long> CreateOrUpdate(CreateUpdateStoreProductsDto input);
        Task<long> Create2(CreateUpdateStoreProductsDto input);

        Task<GetStoreProductsOutputDto> GetById(GetStoreProductsInputDto input);

        Task<GetStoreProductsOutputDto> GetProductById(long Id);

        Task<List<GetStoreProductsOutputDto>> GetVariantsByProductId(long Id);

        Task<List<GetStoreProductsOutputDto>> GetVariantProductById(long Id);

        Task<ListResultDto<GetStoreProductsOutputDto>> GetAdvanceSearch(StoreProductsAdvanceSearchInputDto input);

    }
}
