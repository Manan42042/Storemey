using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreInventoryPurchaseLinks.Dto;

namespace Storemey.StoreInventoryPurchaseLinks
{

    public interface IStoreInventoryPurchaseLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreInventoryPurchaseLinksInputDto input);

        Task Update(UpdateStoreInventoryPurchaseLinksInputDto input);

        Task Delete(DeleteStoreInventoryPurchaseLinksInputDto input);

        Task<GetStoreInventoryPurchaseLinksOutputDto> GetById(GetStoreInventoryPurchaseLinksInputDto input);

        Task<ListResultDto<GetStoreInventoryPurchaseLinksOutputDto>> GetAdvanceSearch(StoreInventoryPurchaseLinksAdvanceSearchInputDto input);

    }
}
