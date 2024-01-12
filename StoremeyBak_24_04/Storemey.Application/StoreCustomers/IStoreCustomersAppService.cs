using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCustomers.Dto;

namespace Storemey.StoreCustomers
{

    public interface IStoreCustomersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCustomersOutputDto>> ListAll();
        
        Task Create(CreateStoreCustomersInputDto input);

        Task Update(UpdateStoreCustomersInputDto input);

        Task Delete(DeleteStoreCustomersInputDto input);

        Task<GetStoreCustomersOutputDto> GetById(GetStoreCustomersInputDto input);

        Task<ListResultDto<GetStoreCustomersOutputDto>> GetAdvanceSearch(StoreCustomersAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
