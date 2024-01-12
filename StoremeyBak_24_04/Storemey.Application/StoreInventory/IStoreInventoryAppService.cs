using System;
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

        Task<GetStoreInventoryOutputDto> GetById(GetStoreInventoryInputDto input);

        Task<ListResultDto<GetStoreInventoryOutputDto>> GetAdvanceSearch(StoreInventoryAdvanceSearchInputDto input);

  
    }
}
