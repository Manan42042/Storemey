using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreRoles
{
    public interface IStoreRolesManager : IDomainService
    {
        Task<IEnumerable<StoreRoles>> ListAll();

        Task<IQueryable<StoreRoles>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreRoles input);

        Task Update(StoreRoles input);

        Task Delete(StoreRoles input);

        Task <StoreRoles> GetByID(long ID);
        
    }
}