using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.MasterCountries.Dto;

namespace Storemey.MasterCountries
{

    public interface IMasterCountriesAppService : IApplicationService
    {
        Task<ListResultDto<GetMasterCountriesOutputDto>> ListAll();
        
        Task Create(CreateMasterCountriesInputDto input);

        Task Update(UpdateMasterCountriesInputDto input);

        Task Delete(DeleteMasterCountriesInputDto input);

        Task<GetMasterCountriesOutputDto> GetById(GetMasterCountriesInputDto input);

        Task<ListResultDto<GetMasterCountriesOutputDto>> GetAdvanceSearch(MasterCountriesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
