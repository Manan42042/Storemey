using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminBugTrackerComments.Dto;

namespace Storemey.AdminBugTrackerComments
{

    public interface IAdminBugTrackerCommentsAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminBugTrackerCommentsOutputDto>> ListAll();

        
        Task Create(CreateAdminBugTrackerCommentsInputDto input);

        Task Update(UpdateAdminBugTrackerCommentsInputDto input);

        Task Delete(DeleteAdminBugTrackerCommentsInputDto input);

        Task<GetAdminBugTrackerCommentsOutputDto> GetById(GetAdminBugTrackerCommentsInputDto input);

        Task<ListResultDto<GetAdminBugTrackerCommentsOutputDto>> GetAdvanceSearch(AdminBugTrackerCommentsAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
