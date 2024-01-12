using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreWarehouses.Dto;

namespace Storemey.StoreWarehouses
{

    public interface IStoreWarehousesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreWarehousesOutputDto>> ListAll();
        
        Task Create(CreateStoreWarehousesInputDto input);

        Task Update(UpdateStoreWarehousesInputDto input);

        Task Delete(DeleteStoreWarehousesInputDto input);

        Task<GetStoreWarehousesOutputDto> GetById(GetStoreWarehousesInputDto input);

        Task<ListResultDto<GetStoreWarehousesOutputDto>> GetAdvanceSearch(StoreWarehousesAdvanceSearchInputDto input);

    }
}
