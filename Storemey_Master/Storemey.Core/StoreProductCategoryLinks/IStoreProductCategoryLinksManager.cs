﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductCategoryLinks
{
    public interface IStoreProductCategoryLinksManager : IDomainService
    {
        Task<IEnumerable<StoreProductCategoryLinks>> ListAll();

        Task<Tuple<IQueryable<StoreProductCategoryLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task Create(StoreProductCategoryLinks input);

        Task Update(StoreProductCategoryLinks input);

        Task Delete(StoreProductCategoryLinks input);

        Task <StoreProductCategoryLinks> GetByID(long ID);

        Task<List<StoreProductCategoryLinks>> GetByProductId(long ID);

        Task DeleteByProductId(long ProductId);


    }
}