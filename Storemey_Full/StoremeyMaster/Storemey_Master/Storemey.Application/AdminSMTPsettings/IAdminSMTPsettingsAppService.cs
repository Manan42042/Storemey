using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminSMTPsettings.Dto;

namespace Storemey.AdminSMTPsettings
{

    public interface IAdminSMTPsettingsAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminSMTPsettingsOutputDto>> ListAll();
        
        Task Create(CreateAdminSMTPsettingsInputDto input);

        Task Update(UpdateAdminSMTPsettingsInputDto input);

        Task Delete(DeleteAdminSMTPsettingsInputDto input);

        Task<GetAdminSMTPsettingsOutputDto> GetById(GetAdminSMTPsettingsInputDto input);

        Task<ListResultDto<GetAdminSMTPsettingsOutputDto>> GetAdvanceSearch(AdminSMTPsettingsAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
