using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventoryTransferLinks
{
    public interface IStoreInventoryTransferLinksManager : IDomainService
    {
        Task<IEnumerable<StoreInventoryTransferLinks>> ListAll();

        Task<Tuple<IQueryable<StoreInventoryTransferLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreInventoryTransferLinks input);

        Task Update(StoreInventoryTransferLinks input);

        Task Delete(StoreInventoryTransferLinks input);

        Task <StoreInventoryTransferLinks> GetByID(long ID);
        
    }
}