using System;
using System.Threading.Tasks;
using System.Web;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Storemey.ProductService.Dto;

namespace Storemey.POSService
{

    public interface IPOSService : IApplicationService
    {
        Task<string> GetsaleById();

        Task<string> SaveOrUpdatesale();

        Task<string> GetMastersdata();
    }
}
