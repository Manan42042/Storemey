using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreUsers
{
    public interface IStoreUsersManager : IDomainService
    {
        Task<IEnumerable<StoreUsers>> ListAll();

        Task<IQueryable<StoreUsers>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreUsers input);

        Task Update(StoreUsers input);

        Task Delete(StoreUsers input);

        Task <StoreUsers> GetByID(long ID);
        
    }
}