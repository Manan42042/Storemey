using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreGiftCards.Dto;

namespace Storemey.StoreGiftCards
{

    public interface IStoreGiftCardsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreGiftCardsOutputDto>> ListAll();
        
        Task Create(CreateStoreGiftCardsInputDto input);

        Task Update(UpdateStoreGiftCardsInputDto input);

        Task Delete(DeleteStoreGiftCardsInputDto input);

        Task<GetStoreGiftCardsOutputDto> GetById(GetStoreGiftCardsInputDto input);

        Task<ListResultDto<GetStoreGiftCardsOutputDto>> GetAdvanceSearch(StoreGiftCardsAdvanceSearchInputDto input);

    }
}
