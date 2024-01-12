using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTaxGroupLinks.Dto;

namespace Storemey.StoreTaxGroupLinks
{

    public interface IStoreTaxGroupLinksAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTaxGroupLinksOutputDto>> ListAll();
        
        Task Create(CreateStoreTaxGroupLinksInputDto input);

        Task Update(UpdateStoreTaxGroupLinksInputDto input);

        Task Delete(DeleteStoreTaxGroupLinksInputDto input);

        Task<GetStoreTaxGroupLinksOutputDto> GetById(GetStoreTaxGroupLinksInputDto input);

        Task<ListResultDto<GetStoreTaxGroupLinksOutputDto>> GetAdvanceSearch(StoreTaxGroupLinksAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
