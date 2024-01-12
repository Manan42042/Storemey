using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminUpdateAllDatabase
{
    public interface IAdminUpdateAllDatabaseManager : IDomainService
    {
        Task<IEnumerable<AdminUpdateAllDatabase>> ListAll();

        Task<IQueryable<AdminUpdateAllDatabase>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        

        Task Create(AdminUpdateAllDatabase input);

        Task Update(AdminUpdateAllDatabase input);

        Task Delete(AdminUpdateAllDatabase input);

        Task CreateOrUpdate(AdminUpdateAllDatabase input);

        Task <AdminUpdateAllDatabase> GetByID(long ID);

    }
}