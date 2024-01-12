using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.MasterPlanPrices.Dto;

namespace Storemey.MasterPlanPrices
{
    public interface IMasterPlanPricesAppService : IApplicationService
    {
        Task<ListResultDto<GetMasterPlanPricesOutputDto>> ListAll();
        
        Task Create(CreateMasterPlanPricesInputDto input);

        Task Update(UpdateMasterPlanPricesInputDto input);

        Task Delete(DeleteMasterPlanPricesInputDto input);

        Task<GetMasterPlanPricesOutputDto> GetById(GetMasterPlanPricesInputDto input);

        Task<ListResultDto<GetMasterPlanPricesOutputDto>> GetAdvanceSearch(MasterPlanPricesAdvanceSearchInputDto input);
    }
}
