using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.StoreBrands.Dto;

namespace Storemey.StoreBrands
{

    public interface IStoreBrandsAppService : IApplicationService
    {
        Task<ListResultDto<GetStoreBrandsOutputDto>> ListAll();
        
        Task Create(CreateStoreBrandsInputDto input);

        Task Update(UpdateStoreBrandsInputDto input);

        Task Delete(DeleteStoreBrandsInputDto input);

        Task<GetStoreBrandsOutputDto> GetById(GetStoreBrandsInputDto input);

        Task<ListResultDto<GetStoreBrandsOutputDto>> GetAdvanceSearch(StoreBrandsAdvanceSearchInputDto input);

    }
}
