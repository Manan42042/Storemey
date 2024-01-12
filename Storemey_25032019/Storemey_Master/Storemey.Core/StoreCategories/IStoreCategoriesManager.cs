using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCategories
{
    public interface IStoreCategoriesManager : IDomainService
    {
        Task<IEnumerable<StoreCategories>> ListAll();

        Task<IQueryable<StoreCategories>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreCategories input);

        Task Update(StoreCategories input);

        Task Delete(StoreCategories input);

        Task <StoreCategories> GetByID(long ID);
        
    }
}