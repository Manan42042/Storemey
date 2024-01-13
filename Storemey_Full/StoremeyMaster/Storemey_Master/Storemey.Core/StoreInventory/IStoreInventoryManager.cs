using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventory
{
    public interface IStoreInventoryManager : IDomainService
    {
        Task<IEnumerable<StoreInventory>> ListAll();

        Task<List<StoreInventory>> ListAllProductAndOutlet(long ProductId, long OutletId);

        Task<Tuple<IQueryable<StoreInventory>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        Task DeleteByProductId(long ProductId);
        Task Create(StoreInventory input);

        Task Update(StoreInventory input);

        Task Delete(StoreInventory input);

        Task <StoreInventory> GetByID(long ID);
        
    }
}