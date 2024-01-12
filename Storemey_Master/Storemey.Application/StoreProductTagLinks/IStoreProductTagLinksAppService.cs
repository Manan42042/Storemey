using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductTagLinks.Dto;

namespace Storemey.StoreProductTagLinks
{

    public interface IStoreProductTagLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductTagLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreProductTagLinksInputDto input);

        Task Update(UpdateStoreProductTagLinksInputDto input);

        Task Delete(DeleteStoreProductTagLinksInputDto input);

        Task<GetStoreProductTagLinksOutputDto> GetById(GetStoreProductTagLinksInputDto input);

        Task<ListResultDto<GetStoreProductTagLinksOutputDto>> GetAdvanceSearch(StoreProductTagLinksAdvanceSearchInputDto input);


        Task CreateProductLinks(long ProductId, long Id);

        Task DeleteByProductId(long ProductId);

        Task<List<GetStoreProductTagLinksOutputDto>> GetByProductId(long ProductId);

    }
}
