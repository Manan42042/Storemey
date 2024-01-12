using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreInventoryPurchaseOrders.Dto;

namespace Storemey.StoreInventoryPurchaseOrders
{

    public interface IStoreInventoryPurchaseOrdersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>> ListAll();
        
        Task Create(CreateStoreInventoryPurchaseOrdersInputDto input);

        Task Update(UpdateStoreInventoryPurchaseOrdersInputDto input);

        Task Delete(DeleteStoreInventoryPurchaseOrdersInputDto input);

        Task<GetStoreInventoryPurchaseOrdersOutputDto> GetById(GetStoreInventoryPurchaseOrdersInputDto input);

        Task<ListResultDto<GetStoreInventoryPurchaseOrdersOutputDto>> GetAdvanceSearch(StoreInventoryPurchaseOrdersAdvanceSearchInputDto input);

    }
}
