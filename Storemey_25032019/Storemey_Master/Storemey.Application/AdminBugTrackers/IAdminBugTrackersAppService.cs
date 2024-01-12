using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminBugTrackers.Dto;

namespace Storemey.AdminBugTrackers
{

    public interface IAdminBugTrackersAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminBugTrackersOutputDto>> ListAll();
        
        Task Create(CreateAdminBugTrackersInputDto input);

        Task Update(UpdateAdminBugTrackersInputDto input);

        Task Delete(DeleteAdminBugTrackersInputDto input);

        Task<GetAdminBugTrackersOutputDto> GetById(GetAdminBugTrackersInputDto input);

        Task<ListResultDto<GetAdminBugTrackersOutputDto>> GetAdvanceSearch(AdminBugTrackersAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
