﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCategories
{
    public interface IStoreCategoriesManager : IDomainService
    {
        Task<IEnumerable<StoreCategories>> ListAll();

        Task<Tuple<IQueryable<StoreCategories>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreCategories input);

        Task Update(StoreCategories input);

        Task Delete(StoreCategories input);

        Task <StoreCategories> GetByID(long ID);

        Task<long> GetMaxID(long ID);


    }
}