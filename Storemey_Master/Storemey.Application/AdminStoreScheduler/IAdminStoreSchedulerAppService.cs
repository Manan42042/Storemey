using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminStoreScheduler.Dto;

namespace Storemey.AdminStoreScheduler
{

    public interface IAdminStoreSchedulerAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminStoreSchedulerOutputDto>> ListAll();
        
        Task Create(CreateAdminStoreSchedulerInputDto input);

        Task Update(UpdateAdminStoreSchedulerInputDto input);

        Task Delete(DeleteAdminStoreSchedulerInputDto input);

        Task<GetAdminStoreSchedulerOutputDto> GetById(GetAdminStoreSchedulerInputDto input);

        Task<ListResultDto<GetAdminStoreSchedulerOutputDto>> GetAdvanceSearch(AdminStoreSchedulerAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
