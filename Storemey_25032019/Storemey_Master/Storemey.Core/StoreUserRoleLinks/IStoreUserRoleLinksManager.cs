using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreUserRoleLinks
{
    public interface IStoreUserRoleLinksManager : IDomainService
    {
        Task<IEnumerable<StoreUserRoleLinks>> ListAll();

        Task<IQueryable<StoreUserRoleLinks>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreUserRoleLinks input);

        Task Update(StoreUserRoleLinks input);

        Task Delete(StoreUserRoleLinks input);

        Task <StoreUserRoleLinks> GetByID(long ID);
        
    }
}