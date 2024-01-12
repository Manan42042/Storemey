using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreSalePayments.Dto;

namespace Storemey.StoreSalePayments
{

    public interface IStoreSalePaymentsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreSalePaymentsOutputDto>> ListAll();
        
        Task Create(CreateStoreSalePaymentsInputDto input);

        Task Update(UpdateStoreSalePaymentsInputDto input);

        Task Delete(DeleteStoreSalePaymentsInputDto input);

        Task<GetStoreSalePaymentsOutputDto> GetById(GetStoreSalePaymentsInputDto input);

        Task<ListResultDto<GetStoreSalePaymentsOutputDto>> GetAdvanceSearch(StoreSalePaymentsAdvanceSearchInputDto input);

    }
}
