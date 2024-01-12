using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreSeasons.Dto;

namespace Storemey.StoreSeasons
{

    public interface IStoreSeasonsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreSeasonsOutputDto>> ListAll();
        
        Task Create(CreateStoreSeasonsInputDto input);

        Task Update(UpdateStoreSeasonsInputDto input);

        Task Delete(DeleteStoreSeasonsInputDto input);

        Task<GetStoreSeasonsOutputDto> GetById(GetStoreSeasonsInputDto input);

        Task<ListResultDto<GetStoreSeasonsOutputDto>> GetAdvanceSearch(StoreSeasonsAdvanceSearchInputDto input);

    }
}
