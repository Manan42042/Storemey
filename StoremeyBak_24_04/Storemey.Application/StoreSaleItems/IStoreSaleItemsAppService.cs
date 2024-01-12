using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreSaleItems.Dto;

namespace Storemey.StoreSaleItems
{

    public interface IStoreSaleItemsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreSaleItemsOutputDto>> ListAll();
        
        Task Create(CreateStoreSaleItemsInputDto input);

        Task Update(UpdateStoreSaleItemsInputDto input);

        Task Delete(DeleteStoreSaleItemsInputDto input);

        Task<GetStoreSaleItemsOutputDto> GetById(GetStoreSaleItemsInputDto input);

        Task<ListResultDto<GetStoreSaleItemsOutputDto>> GetAdvanceSearch(StoreSaleItemsAdvanceSearchInputDto input);

    }
}
