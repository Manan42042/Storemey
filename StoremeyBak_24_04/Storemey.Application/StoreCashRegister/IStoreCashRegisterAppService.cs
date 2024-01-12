using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCashRegister.Dto;

namespace Storemey.StoreCashRegister
{

    public interface IStoreCashRegisterAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCashRegisterOutputDto>> ListAll();
        
        Task Create(CreateStoreCashRegisterInputDto input);

        Task Update(UpdateStoreCashRegisterInputDto input);

        Task Delete(DeleteStoreCashRegisterInputDto input);

        Task<GetStoreCashRegisterOutputDto> GetById(GetStoreCashRegisterInputDto input);

        Task<ListResultDto<GetStoreCashRegisterOutputDto>> GetAdvanceSearch(StoreCashRegisterAdvanceSearchInputDto input);

    }
}
