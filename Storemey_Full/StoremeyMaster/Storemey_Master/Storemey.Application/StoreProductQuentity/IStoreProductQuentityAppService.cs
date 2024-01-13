using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductQuentity.Dto;

namespace Storemey.StoreProductQuentity
{

    public interface IStoreProductQuentityAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductQuentityOutputDto>> ListAll();
        
        Task Create(CreateStoreProductQuentityInputDto input);

        Task Update(UpdateStoreProductQuentityInputDto input);

        Task Delete(DeleteStoreProductQuentityInputDto input);

        Task<GetStoreProductQuentityOutputDto> GetById(GetStoreProductQuentityInputDto input);

        Task<List<GetStoreProductQuentityOutputDto>> GetByProductAndOutlet(long ProductId, long OutletId);

        Task<ListResultDto<GetStoreProductQuentityOutputDto>> GetAdvanceSearch(StoreProductQuentityAdvanceSearchInputDto input);

    }
}
