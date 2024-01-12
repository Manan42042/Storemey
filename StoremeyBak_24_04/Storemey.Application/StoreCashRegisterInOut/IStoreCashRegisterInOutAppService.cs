using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCashRegisterInOut.Dto;

namespace Storemey.StoreCashRegisterInOut
{

    public interface IStoreCashRegisterInOutAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCashRegisterInOutOutputDto>> ListAll();
        
        Task Create(CreateStoreCashRegisterInOutInputDto input);

        Task Update(UpdateStoreCashRegisterInOutInputDto input);

        Task Delete(DeleteStoreCashRegisterInOutInputDto input);

        Task<GetStoreCashRegisterInOutOutputDto> GetById(GetStoreCashRegisterInOutInputDto input);

        Task<ListResultDto<GetStoreCashRegisterInOutOutputDto>> GetAdvanceSearch(StoreCashRegisterInOutAdvanceSearchInputDto input);

    }
}
