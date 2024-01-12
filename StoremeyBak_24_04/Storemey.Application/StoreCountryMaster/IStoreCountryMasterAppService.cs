using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCountryMaster.Dto;

namespace Storemey.StoreCountryMaster
{

    public interface IStoreCountryMasterAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCountryMasterOutputDto>> ListAll();
        
        Task Create(CreateStoreCountryMasterInputDto input);

        Task Update(UpdateStoreCountryMasterInputDto input);

        Task Delete(DeleteStoreCountryMasterInputDto input);

        Task<GetStoreCountryMasterOutputDto> GetById(GetStoreCountryMasterInputDto input);

        Task<ListResultDto<GetStoreCountryMasterOutputDto>> GetAdvanceSearch(StoreCountryMasterAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
