using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminEmailTemplates.Dto;

namespace Storemey.AdminEmailTemplates
{

    public interface IAdminEmailTemplatesAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminEmailTemplatesOutputDto>> ListAll();
        
        Task Create(CreateAdminEmailTemplatesInputDto input);

        Task Update(UpdateAdminEmailTemplatesInputDto input);

        Task Delete(DeleteAdminEmailTemplatesInputDto input);

        Task<GetAdminEmailTemplatesOutputDto> GetById(GetAdminEmailTemplatesInputDto input);

        Task<ListResultDto<GetAdminEmailTemplatesOutputDto>> GetAdvanceSearch(AdminEmailTemplatesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
