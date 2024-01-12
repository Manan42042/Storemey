﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreInventory
{
    public interface IStoreInventoryManager : IDomainService
    {
        Task<IEnumerable<StoreInventory>> ListAll();

        Task<IQueryable<StoreInventory>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreInventory input);

        Task Update(StoreInventory input);

        Task Delete(StoreInventory input);

        Task <StoreInventory> GetByID(long ID);
        
    }
}