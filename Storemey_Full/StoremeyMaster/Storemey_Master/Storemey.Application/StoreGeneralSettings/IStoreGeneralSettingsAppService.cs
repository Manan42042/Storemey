using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreGeneralSettings.Dto;

namespace Storemey.StoreGeneralSettings
{

    public interface IStoreGeneralSettingsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreGeneralSettingsOutputDto>> ListAll();
        
        Task Create(CreateStoreGeneralSettingsInputDto input);

        Task Update(UpdateStoreGeneralSettingsInputDto input);

        Task Delete(DeleteStoreGeneralSettingsInputDto input);

        Task<GetStoreGeneralSettingsOutputDto> GetById(GetStoreGeneralSettingsInputDto input);


    }
}
