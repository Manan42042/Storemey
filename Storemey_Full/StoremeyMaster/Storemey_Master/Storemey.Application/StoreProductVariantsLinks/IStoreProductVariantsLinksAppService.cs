using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductVariantsLinks.Dto;

namespace Storemey.StoreProductVariantsLinks
{

    public interface IStoreProductVariantsLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductVariantsLinksOutputDto>> ListAll();

        Task Create(CreateStoreProductVariantsLinksInputDto input);


        Task Update(UpdateStoreProductVariantsLinksInputDto input);

        Task Delete(DeleteStoreProductVariantsLinksInputDto input);

        Task<GetStoreProductVariantsLinksOutputDto> GetById(GetStoreProductVariantsLinksInputDto input);

        Task<List<GetStoreProductVariantsLinksOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId);

        Task<ListResultDto<GetStoreProductVariantsLinksOutputDto>> GetAdvanceSearch(StoreProductVariantsLinksAdvanceSearchInputDto input);

        Task CreateValriantLink(long ProductId, long VariantId, long VariantValueId);

        Task DeleteByProductId(long ProductId);

        Task<List<GetStoreProductVariantsLinksOutputDto>> GetByProductId(long ProductId);

    }
}
