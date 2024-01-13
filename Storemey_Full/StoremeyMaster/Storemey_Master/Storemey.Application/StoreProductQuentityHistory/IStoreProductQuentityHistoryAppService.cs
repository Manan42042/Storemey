using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductQuentityHistory.Dto;

namespace Storemey.StoreProductQuentityHistory
{

    public interface IStoreProductQuentityHistoryAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductQuentityHistoryOutputDto>> ListAll();
        
        Task Create(CreateStoreProductQuentityHistoryInputDto input);

        Task Update(UpdateStoreProductQuentityHistoryInputDto input);

        Task Delete(DeleteStoreProductQuentityHistoryInputDto input);

        Task<GetStoreProductQuentityHistoryOutputDto> GetById(GetStoreProductQuentityHistoryInputDto input);

        Task<List<GetStoreProductQuentityHistoryOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId);

        Task<ListResultDto<GetStoreProductQuentityHistoryOutputDto>> GetAdvanceSearch(StoreProductQuentityHistoryAdvanceSearchInputDto input);

    }
}
