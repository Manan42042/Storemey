using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventoryPurchaseLinks
{
    public interface IStoreInventoryPurchaseLinksManager : IDomainService
    {
        Task<IEnumerable<StoreInventoryPurchaseLinks>> ListAll();

        Task<IQueryable<StoreInventoryPurchaseLinks>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreInventoryPurchaseLinks input);

        Task Update(StoreInventoryPurchaseLinks input);

        Task Delete(StoreInventoryPurchaseLinks input);

        Task <StoreInventoryPurchaseLinks> GetByID(long ID);
        
    }
}