using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.AdminStores.Dto;

namespace Storemey.AdminStores
{

    public interface IAdminStoresAppService : IApplicationService
    {
        Task<ListResultDto<GetAdminStoresOutputDto>> ListAll();
        
        Task Create(CreateAdminStoresInputDto input);

        Task Update(UpdateAdminStoresInputDto input);

        Task Delete(DeleteAdminStoresInputDto input);

        Task<GetAdminStoresOutputDto> GetById(GetAdminStoresInputDto input);

        Task<GetAdminStoresOutputDto> GetByStoreName(string storename);

        Task<ListResultDto<GetAdminStoresOutputDto>> GetAdvanceSearch(AdminStoresAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
