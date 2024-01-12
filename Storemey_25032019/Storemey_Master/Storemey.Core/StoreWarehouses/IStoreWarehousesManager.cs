﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreWarehouses
{
    public interface IStoreWarehousesManager : IDomainService
    {
        Task<IEnumerable<StoreWarehouses>> ListAll();

        Task<IQueryable<StoreWarehouses>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreWarehouses input);

        Task CreateOrUpdate(StoreWarehouses input);

        Task Update(StoreWarehouses input);

        Task Delete(StoreWarehouses input);

        Task <StoreWarehouses> GetByID(long ID);
        
    }
}