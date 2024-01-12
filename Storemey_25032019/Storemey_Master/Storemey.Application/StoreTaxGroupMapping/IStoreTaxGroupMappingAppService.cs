using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTaxGroupMapping.Dto;

namespace Storemey.StoreTaxGroupMapping
{

    public interface IStoreTaxGroupMappingAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTaxGroupMappingOutputDto>> ListAll();
        
        Task Create(CreateStoreTaxGroupMappingInputDto input);

        Task Update(UpdateStoreTaxGroupMappingInputDto input);

        Task Delete(DeleteStoreTaxGroupMappingInputDto input);

        Task<GetStoreTaxGroupMappingOutputDto> GetById(GetStoreTaxGroupMappingInputDto input);

        Task<ListResultDto<GetStoreTaxGroupMappingOutputDto>> GetAdvanceSearch(StoreTaxGroupMappingAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
