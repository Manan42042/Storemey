using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCurrencies.Dto;

namespace Storemey.StoreCurrencies
{

    public interface IStoreCurrenciesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCurrenciesOutputDto>> ListAll();
        
        Task Create(CreateStoreCurrenciesInputDto input);

        Task Update(UpdateStoreCurrenciesInputDto input);

        Task Delete(DeleteStoreCurrenciesInputDto input);

        Task<GetStoreCurrenciesOutputDto> GetById(GetStoreCurrenciesInputDto input);

        Task<ListResultDto<GetStoreCurrenciesOutputDto>> GetAdvanceSearch(StoreCurrenciesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
