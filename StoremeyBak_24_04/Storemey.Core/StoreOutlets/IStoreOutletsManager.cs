using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreOutlets
{
    public interface IStoreOutletsManager : IDomainService
    {
        Task<IEnumerable<StoreOutlets>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreOutlets input);

        Task CreateOrUpdate(StoreOutlets input);

        Task Update(StoreOutlets input);

        Task Delete(StoreOutlets input);

        Task <StoreOutlets> GetByID(long ID);
        
    }
}