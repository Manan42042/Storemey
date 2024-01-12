﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTimeZones
{
    public interface IStoreTimeZonesManager : IDomainService
    {
        Task<IEnumerable<StoreTimeZones>> ListAll();

        Task<Tuple<IQueryable<StoreTimeZones>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task Create(StoreTimeZones input);

        Task CreateOrUpdate(StoreTimeZones input);

        Task Update(StoreTimeZones input);

        Task Delete(StoreTimeZones input);

        Task <StoreTimeZones> GetByID(long ID);
        
    }
}