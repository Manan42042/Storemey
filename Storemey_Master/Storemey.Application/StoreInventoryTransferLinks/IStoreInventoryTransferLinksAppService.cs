using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreInventoryTransferLinks.Dto;

namespace Storemey.StoreInventoryTransferLinks
{

    public interface IStoreInventoryTransferLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreInventoryTransferLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreInventoryTransferLinksInputDto input);

        Task Update(UpdateStoreInventoryTransferLinksInputDto input);

        Task Delete(DeleteStoreInventoryTransferLinksInputDto input);

        Task<GetStoreInventoryTransferLinksOutputDto> GetById(GetStoreInventoryTransferLinksInputDto input);

        Task<ListResultDto<GetStoreInventoryTransferLinksOutputDto>> GetAdvanceSearch(StoreInventoryTransferLinksAdvanceSearchInputDto input);

    }
}
