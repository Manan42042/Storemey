﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventoryPurchaseOrders
{
    public interface IStoreInventoryPurchaseOrdersManager : IDomainService
    {
        Task<IEnumerable<StoreInventoryPurchaseOrders>> ListAll();

        Task<IQueryable<StoreInventoryPurchaseOrders>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreInventoryPurchaseOrders input);

        Task Update(StoreInventoryPurchaseOrders input);

        Task Delete(StoreInventoryPurchaseOrders input);

        Task <StoreInventoryPurchaseOrders> GetByID(long ID);
        
    }
}