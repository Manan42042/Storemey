using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductVariantsLinks
{
    public interface IStoreProductVariantsLinksManager : IDomainService
    {
        Task<IEnumerable<StoreProductVariantsLinks>> ListAll();

        Task<List<StoreProductVariantsLinks>> ListAllProductAndOutlet(long ProductId, long OutletId);

        Task<Tuple<IQueryable<StoreProductVariantsLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductVariantsLinks input);

        Task Update(StoreProductVariantsLinks input);

        Task Delete(StoreProductVariantsLinks input);

        Task <StoreProductVariantsLinks> GetByID(long ID);

        Task<List<StoreProductVariantsLinks>> GetByProductId(long ID);

        Task DeleteByProductId(long ProductId);

    }
}