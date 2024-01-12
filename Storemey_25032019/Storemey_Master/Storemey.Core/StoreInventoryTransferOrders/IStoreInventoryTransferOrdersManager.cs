using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventoryTransferOrders
{
    public interface IStoreInventoryTransferOrdersManager : IDomainService
    {
        Task<IEnumerable<StoreInventoryTransferOrders>> ListAll();

        Task<IQueryable<StoreInventoryTransferOrders>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreInventoryTransferOrders input);

        Task Update(StoreInventoryTransferOrders input);

        Task Delete(StoreInventoryTransferOrders input);

        Task <StoreInventoryTransferOrders> GetByID(long ID);
        
    }
}