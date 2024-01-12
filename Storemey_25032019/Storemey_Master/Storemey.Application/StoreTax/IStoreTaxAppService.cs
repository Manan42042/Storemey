using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTax.Dto;

namespace Storemey.StoreTax
{

    public interface IStoreTaxAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTaxOutputDto>> ListAll();
        
        Task Create(CreateStoreTaxInputDto input);

        Task Update(UpdateStoreTaxInputDto input);

        Task Delete(DeleteStoreTaxInputDto input);

        Task<GetStoreTaxOutputDto> GetById(GetStoreTaxInputDto input);

        Task<ListResultDto<GetStoreTaxOutputDto>> GetAdvanceSearch(StoreTaxAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
