using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminBugTrackers
{
    public interface IAdminBugTrackersManager : IDomainService
    {
        Task<IEnumerable<AdminBugTrackers>> ListAll();

        Task<IQueryable<AdminBugTrackers>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(AdminBugTrackers input);

        Task Update(AdminBugTrackers input);

        Task Delete(AdminBugTrackers input);

        Task CreateOrUpdate(AdminBugTrackers input);

        Task <AdminBugTrackers> GetByID(long ID);

        
    }
}