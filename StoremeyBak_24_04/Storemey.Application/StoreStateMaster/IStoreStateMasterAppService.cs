using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreStateMaster.Dto;

namespace Storemey.StoreStateMaster
{

    public interface IStoreStateMasterAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAll();

        Task<ListResultDto<GetStoreStateMasterOutputDto>> ListAllByCountryID(long Id);


        Task Create(CreateStoreStateMasterInputDto input);

        Task Update(UpdateStoreStateMasterInputDto input);

        Task Delete(DeleteStoreStateMasterInputDto input);

        Task<GetStoreStateMasterOutputDto> GetById(GetStoreStateMasterInputDto input);

        Task<ListResultDto<GetStoreStateMasterOutputDto>> GetAdvanceSearch(StoreStateMasterAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
