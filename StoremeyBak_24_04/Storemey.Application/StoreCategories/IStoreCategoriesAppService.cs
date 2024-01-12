using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreCategories.Dto;

namespace Storemey.StoreCategories
{

    public interface IStoreCategoriesAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreCategoriesOutputDto>> ListAll();
        
        Task Create(CreateStoreCategoriesInputDto input);

        Task Update(UpdateStoreCategoriesInputDto input);

        Task Delete(DeleteStoreCategoriesInputDto input);

        Task<GetStoreCategoriesOutputDto> GetById(GetStoreCategoriesInputDto input);

        Task<ListResultDto<GetStoreCategoriesOutputDto>> GetAdvanceSearch(StoreCategoriesAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
