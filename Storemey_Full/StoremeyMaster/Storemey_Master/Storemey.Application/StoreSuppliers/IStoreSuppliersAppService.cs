using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreSuppliers.Dto;

namespace Storemey.StoreSuppliers
{

    public interface IStoreSuppliersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreSuppliersOutputDto>> ListAll();
        
        Task Create(CreateStoreSuppliersInputDto input);

        Task Update(UpdateStoreSuppliersInputDto input);

        Task Delete(DeleteStoreSuppliersInputDto input);

        Task<GetStoreSuppliersOutputDto> GetById(GetStoreSuppliersInputDto input);

        Task<ListResultDto<GetStoreSuppliersOutputDto>> GetAdvanceSearch(StoreSuppliersAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
