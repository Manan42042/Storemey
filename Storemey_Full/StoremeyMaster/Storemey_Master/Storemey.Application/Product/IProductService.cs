using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.ProductService.Dto;

namespace Storemey.ProductService
{

    public interface IProductService : IApplicationService
    {
        Task<GetStoreProductsDto> GetProductById(GetStoreProductsDto input);

        Task SaveOrUpdateProduct(CreateUpdateStoreProductsDto input);

        Task<GetStoreMastersDto> GetMastersdata();
    }
}
