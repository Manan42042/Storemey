using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminUpdateAllDatabase.Dto;

namespace Storemey.AdminUpdateAllDatabase
{

    public interface IAdminUpdateAllDatabaseAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminUpdateAllDatabaseOutputDto>> ListAll();
        
        Task Create(CreateAdminUpdateAllDatabaseInputDto input);

        Task Update(UpdateAdminUpdateAllDatabaseInputDto input);

        Task Delete(DeleteAdminUpdateAllDatabaseInputDto input);

        Task<GetAdminUpdateAllDatabaseOutputDto> GetById(GetAdminUpdateAllDatabaseInputDto input);

        Task<ListResultDto<GetAdminUpdateAllDatabaseOutputDto>> GetAdvanceSearch(AdminUpdateAllDatabaseAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
