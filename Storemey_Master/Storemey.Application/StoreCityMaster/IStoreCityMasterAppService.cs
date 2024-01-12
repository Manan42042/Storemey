using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCityMaster.Dto;

namespace Storemey.StoreCityMaster
{

    public interface IStoreCityMasterAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAll();
        Task<ListResultDto<GetStoreCityMasterOutputDto>> ListAllByStateId(long StateID);


        Task Create(CreateStoreCityMasterInputDto input);

        Task Update(UpdateStoreCityMasterInputDto input);

        Task Delete(DeleteStoreCityMasterInputDto input);

        Task<GetStoreCityMasterOutputDto> GetById(GetStoreCityMasterInputDto input);

        Task<ListResultDto<GetStoreCityMasterOutputDto>> GetAdvanceSearch(StoreCityMasterAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
