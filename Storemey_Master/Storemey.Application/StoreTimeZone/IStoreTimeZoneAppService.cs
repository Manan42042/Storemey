using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTimeZones.Dto;

namespace Storemey.StoreTimeZones
{

    public interface IStoreTimeZonesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTimeZonesOutputDto>> ListAll();
        
        Task Create(CreateStoreTimeZonesInputDto input);

        Task Update(UpdateStoreTimeZonesInputDto input);

        Task Delete(DeleteStoreTimeZonesInputDto input);

        Task<GetStoreTimeZonesOutputDto> GetById(GetStoreTimeZonesInputDto input);

        Task<ListResultDto<GetStoreTimeZonesOutputDto>> GetAdvanceSearch(StoreTimeZonesAdvanceSearchInputDto input);

    }
}
