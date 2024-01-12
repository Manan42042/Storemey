using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCustomers
{
    public interface IStoreCustomersManager : IDomainService
    {
        Task<IEnumerable<StoreCustomers>> ListAll();

        Task<IQueryable<StoreCustomers>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreCustomers input);

        Task Update(StoreCustomers input);

        Task Delete(StoreCustomers input);

        Task <StoreCustomers> GetByID(long ID);
        
    }
}