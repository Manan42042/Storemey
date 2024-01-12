using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminStoreScheduler
{
    public interface IAdminStoreSchedulerManager : IDomainService
    {
        Task<IEnumerable<AdminStoreScheduler>> ListAll();

        Task<IQueryable<AdminStoreScheduler>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(AdminStoreScheduler input);

        Task Update(AdminStoreScheduler input);

        Task Delete(AdminStoreScheduler input);

        Task CreateOrUpdate(AdminStoreScheduler input);

        Task <AdminStoreScheduler> GetByID(long ID);

    }
}