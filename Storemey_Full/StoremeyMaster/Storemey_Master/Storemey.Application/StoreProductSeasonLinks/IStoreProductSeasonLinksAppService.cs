using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreProductSeasonLinks.Dto;

namespace Storemey.StoreProductSeasonLinks
{

    public interface IStoreProductSeasonLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreProductSeasonLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreProductSeasonLinksInputDto input);

        Task Update(UpdateStoreProductSeasonLinksInputDto input);

        Task Delete(DeleteStoreProductSeasonLinksInputDto input);

        Task<GetStoreProductSeasonLinksOutputDto> GetById(GetStoreProductSeasonLinksInputDto input);

        Task<ListResultDto<GetStoreProductSeasonLinksOutputDto>> GetAdvanceSearch(StoreProductSeasonLinksAdvanceSearchInputDto input);


        Task CreateProductLinks(long ProductId, long Id);

        Task DeleteByProductId(long ProductId);

        Task<List<GetStoreProductSeasonLinksOutputDto>> GetByProductId(long ProductId);

    }
}
