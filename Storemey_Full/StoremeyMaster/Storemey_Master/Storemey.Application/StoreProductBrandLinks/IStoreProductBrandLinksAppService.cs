using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductBrandLinks.Dto;

namespace Storemey.StoreProductBrandLinks
{

    public interface IStoreProductBrandLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductBrandLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreProductBrandLinksInputDto input);

        Task Update(UpdateStoreProductBrandLinksInputDto input);

        Task Delete(DeleteStoreProductBrandLinksInputDto input);

        Task<GetStoreProductBrandLinksOutputDto> GetById(GetStoreProductBrandLinksInputDto input);

        Task<ListResultDto<GetStoreProductBrandLinksOutputDto>> GetAdvanceSearch(StoreProductBrandLinksAdvanceSearchInputDto input);


        Task CreateProductLinks(long ProductId, long Id);

        Task DeleteByProductId(long ProductId);

        Task<List<GetStoreProductBrandLinksOutputDto>> GetByProductId(long ProductId);

    }
}
