using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminStores
{
    public interface IAdminStoresManager : IDomainService
    {
        Task<IEnumerable<AdminStores>> ListAll();

        Task<IQueryable<AdminStores>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(AdminStores input);

        Task Update(AdminStores input);

        Task Delete(AdminStores input);

        Task CreateOrUpdate(AdminStores input);

        Task <AdminStores> GetByID(long ID);

        Task<AdminStores> GetByStoreName(string StoreName); 

    }
}