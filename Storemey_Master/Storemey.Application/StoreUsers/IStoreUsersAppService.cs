using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreUsers.Dto;

namespace Storemey.StoreUsers
{

    public interface IStoreUsersAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreUsersOutputDto>> ListAll();
        
        Task Create(CreateStoreUsersInputDto input);

        Task Update(UpdateStoreUsersInputDto input);

        Task Delete(DeleteStoreUsersInputDto input);

        Task<GetStoreUsersOutputDto> GetById(GetStoreUsersInputDto input);


        Task<ListResultDto<GetStoreUsersOutputDto>> GetAdvanceSearch(StoreUsersAdvanceSearchInputDto input);

        Task<string> ExportToCSV();

        Task ImportFromCSV(string postedFile);
    }
}
