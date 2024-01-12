using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProducts
{
    public interface IStoreProductsManager : IDomainService
    {
        Task<IEnumerable<StoreProducts>> ListAll();

        Task<IQueryable<StoreProducts>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreProducts input);

        Task Update(StoreProducts input);

        Task Delete(StoreProducts input);

        Task <StoreProducts> GetByID(long ID);
        
    }
}