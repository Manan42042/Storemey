using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.AdminSMTPsettings
{
    public interface IAdminSMTPsettingsManager : IDomainService
    {
        Task<IEnumerable<AdminSMTPsettings>> ListAll();

        Task<IQueryable<AdminSMTPsettings>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(AdminSMTPsettings input);

        Task Update(AdminSMTPsettings input);

        Task Delete(AdminSMTPsettings input);

        Task CreateOrUpdate(AdminSMTPsettings input);

        Task <AdminSMTPsettings> GetByID(long ID);

    }
}