using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreInventoryTransferOrders.Dto;

namespace Storemey.StoreInventoryTransferOrders
{

    public interface IStoreInventoryTransferOrdersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreInventoryTransferOrdersOutputDto>> ListAll();
        
        Task Create(CreateStoreInventoryTransferOrdersInputDto input);

        Task Update(UpdateStoreInventoryTransferOrdersInputDto input);

        Task Delete(DeleteStoreInventoryTransferOrdersInputDto input);

        Task<GetStoreInventoryTransferOrdersOutputDto> GetById(GetStoreInventoryTransferOrdersInputDto input);

        Task<ListResultDto<GetStoreInventoryTransferOrdersOutputDto>> GetAdvanceSearch(StoreInventoryTransferOrdersAdvanceSearchInputDto input);

    }
}
