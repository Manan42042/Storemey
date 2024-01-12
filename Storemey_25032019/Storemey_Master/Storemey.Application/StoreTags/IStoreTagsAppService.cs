using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreTags.Dto;

namespace Storemey.StoreTags
{

    public interface IStoreTagsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreTagsOutputDto>> ListAll();
        
        Task Create(CreateStoreTagsInputDto input);

        Task Update(UpdateStoreTagsInputDto input);

        Task Delete(DeleteStoreTagsInputDto input);

        Task<GetStoreTagsOutputDto> GetById(GetStoreTagsInputDto input);

        Task<ListResultDto<GetStoreTagsOutputDto>> GetAdvanceSearch(StoreTagsAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
