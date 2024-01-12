using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTaxGroups.Dto;

namespace Storemey.StoreTaxGroups
{

    public interface IStoreTaxGroupsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTaxGroupsOutputDto>> ListAll();
        
        Task Create(CreateStoreTaxGroupsInputDto input);

        Task Update(UpdateStoreTaxGroupsInputDto input);

        Task Delete(DeleteStoreTaxGroupsInputDto input);

        Task<GetStoreTaxGroupsOutputDto> GetById(GetStoreTaxGroupsInputDto input);

        Task<ListResultDto<GetStoreTaxGroupsOutputDto>> GetAdvanceSearch(StoreTaxGroupsAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
