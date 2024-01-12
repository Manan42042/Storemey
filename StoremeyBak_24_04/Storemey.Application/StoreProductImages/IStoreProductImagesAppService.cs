using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductImages.Dto;

namespace Storemey.StoreProductImages
{

    public interface IStoreProductImagesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductImagesOutputDto>> ListAll();
        
        Task Create(CreateStoreProductImagesInputDto input);

        Task Update(UpdateStoreProductImagesInputDto input);

        Task Delete(DeleteStoreProductImagesInputDto input);

        Task<GetStoreProductImagesOutputDto> GetById(GetStoreProductImagesInputDto input);

        Task<ListResultDto<GetStoreProductImagesOutputDto>> GetAdvanceSearch(StoreProductImagesAdvanceSearchInputDto input);

    }
}
