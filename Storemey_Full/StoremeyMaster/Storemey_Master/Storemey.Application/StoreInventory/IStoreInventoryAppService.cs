using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreInventory.Dto;

namespace Storemey.StoreInventory
{

    public interface IStoreInventoryAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreInventoryOutputDto>> ListAll();
        
        Task Create(CreateStoreInventoryInputDto input);

        Task Update(UpdateStoreInventoryInputDto input);

        Task Delete(DeleteStoreInventoryInputDto input);

        Task DeleteByProductId(long ProductId);

        Task<GetStoreInventoryOutputDto> GetById(GetStoreInventoryInputDto input);

        Task<List<GetStoreInventoryOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId);

        Task<ListResultDto<GetStoreInventoryOutputDto>> GetAdvanceSearch(StoreInventoryAdvanceSearchInputDto input);

  
    }
}
