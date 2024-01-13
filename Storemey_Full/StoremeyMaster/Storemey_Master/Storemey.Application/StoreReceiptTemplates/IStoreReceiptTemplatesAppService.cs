using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreReceiptTemplates.Dto;

namespace Storemey.StoreReceiptTemplates
{

    public interface IStoreReceiptTemplatesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> ListAll();
        
        Task Create(CreateStoreReceiptTemplatesInputDto input);

        Task Update(UpdateStoreReceiptTemplatesInputDto input);

        Task Delete(DeleteStoreReceiptTemplatesInputDto input);

        Task<GetStoreReceiptTemplatesOutputDto> GetById(GetStoreReceiptTemplatesInputDto input);

        Task<ListResultDto<GetStoreReceiptTemplatesOutputDto>> GetAdvanceSearch(StoreReceiptTemplatesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
