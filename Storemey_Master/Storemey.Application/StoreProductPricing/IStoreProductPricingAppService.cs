using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductPricing.Dto;

namespace Storemey.StoreProductPricing
{

    public interface IStoreProductPricingAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductPricingOutputDto>> ListAll();

        Task Create(CreateStoreProductPricingInputDto input);

        Task Update(UpdateStoreProductPricingInputDto input);

        Task Delete(DeleteStoreProductPricingInputDto input);

        Task DeleteByProductId(long ProductId);

        Task<GetStoreProductPricingOutputDto> GetById(GetStoreProductPricingInputDto input);

        Task<List<GetStoreProductPricingOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId);


        Task<ListResultDto<GetStoreProductPricingOutputDto>> GetAdvanceSearch(StoreProductPricingAdvanceSearchInputDto input);

    }
}
