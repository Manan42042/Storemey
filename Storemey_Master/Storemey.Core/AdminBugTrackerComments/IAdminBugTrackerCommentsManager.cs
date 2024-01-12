using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminBugTrackerComments
{
    public interface IAdminBugTrackerCommentsManager : IDomainService
    {
        Task<IEnumerable<AdminBugTrackerComments>> ListAll();

        Task<IQueryable<AdminBugTrackerComments>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(AdminBugTrackerComments input);

        Task Update(AdminBugTrackerComments input);

        Task Delete(AdminBugTrackerComments input);

        Task CreateOrUpdate(AdminBugTrackerComments input);

        Task <AdminBugTrackerComments> GetByID(long ID);

        
    }
}