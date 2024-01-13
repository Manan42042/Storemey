using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.MasterPlanServices.Dto;

namespace Storemey.MasterPlanServices
{
    public interface IMasterPlanServicesAppService : IApplicationService
    {
        Task<ListResultDto<GetMasterPlanServicesOutputDto>> ListAll();

        ListResultDto<GetMasterPlanServicesOutputDto> GetByPlanId(int Id);

        Task Create(CreateMasterPlanServicesInputDto input);

        Task Update(UpdateMasterPlanServicesInputDto input);

        Task Delete(DeleteMasterPlanServicesInputDto input);

        Task<GetMasterPlanServicesOutputDto> GetById(GetMasterPlanServicesInputDto input);

        Task<ListResultDto<GetMasterPlanServicesOutputDto>> GetAdvanceSearch(MasterPlanServicesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
