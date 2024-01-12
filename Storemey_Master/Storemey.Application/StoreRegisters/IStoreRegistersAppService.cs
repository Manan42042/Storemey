using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreRegisters.Dto;

namespace Storemey.StoreRegisters
{

    public interface IStoreRegistersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreRegistersOutputDto>> ListAll();
        
        Task Create(CreateStoreRegistersInputDto input);

        Task Update(UpdateStoreRegistersInputDto input);

        Task Delete(DeleteStoreRegistersInputDto input);

        Task<GetStoreRegistersOutputDto> GetById(GetStoreRegistersInputDto input);

        Task<ListResultDto<GetStoreRegistersOutputDto>> GetAdvanceSearch(StoreRegistersAdvanceSearchInputDto input);

    }
}
