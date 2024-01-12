using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StorePaymentTypes.Dto;

namespace Storemey.StorePaymentTypes
{

    public interface IStorePaymentTypesAppService : IApplicationService
    {
        Task<ListResultDto<GetStorePaymentTypesOutputDto>> ListAll();
        
        Task Create(CreateStorePaymentTypesInputDto input);

        Task Update(UpdateStorePaymentTypesInputDto input);

        Task Delete(DeleteStorePaymentTypesInputDto input);

        Task<GetStorePaymentTypesOutputDto> GetById(GetStorePaymentTypesInputDto input);

        Task<ListResultDto<GetStorePaymentTypesOutputDto>> GetAdvanceSearch(StorePaymentTypesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
