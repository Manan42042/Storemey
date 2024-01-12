using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminEmailTemplates
{
    public interface IAdminEmailTemplatesManager : IDomainService
    {
        Task<IEnumerable<AdminEmailTemplates>> ListAll();

        Task<IQueryable<AdminEmailTemplates>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(AdminEmailTemplates input);

        Task Update(AdminEmailTemplates input);

        Task Delete(AdminEmailTemplates input);

        Task CreateOrUpdate(AdminEmailTemplates input);

        Task <AdminEmailTemplates> GetByID(long ID);

    }
}