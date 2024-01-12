using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreSaleTransactions.Dto;

namespace Storemey.StoreSaleTransactions
{

    public interface IStoreSaleTransactionsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreSaleTransactionsOutputDto>> ListAll();
        
        Task Create(CreateStoreSaleTransactionsInputDto input);

        Task Update(UpdateStoreSaleTransactionsInputDto input);

        Task Delete(DeleteStoreSaleTransactionsInputDto input);

        Task<GetStoreSaleTransactionsOutputDto> GetById(GetStoreSaleTransactionsInputDto input);

        Task<ListResultDto<GetStoreSaleTransactionsOutputDto>> GetAdvanceSearch(StoreSaleTransactionsAdvanceSearchInputDto input);

    }
}
