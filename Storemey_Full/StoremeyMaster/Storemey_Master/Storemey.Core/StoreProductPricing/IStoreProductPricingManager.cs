using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductPricing
{
    public interface IStoreProductPricingManager : IDomainService
    {
        Task<IEnumerable<StoreProductPricing>> ListAll();

        Task<Tuple<IQueryable<StoreProductPricing>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<List<StoreProductPricing>> ListAllProductAndOutlet(long ProductId, long OutletId);

        Task Create(StoreProductPricing input);

        Task Update(StoreProductPricing input);

        Task Delete(StoreProductPricing input);

        Task <StoreProductPricing> GetByID(long ID);

        Task DeleteByProductId(long ProductId);
      


    }
}