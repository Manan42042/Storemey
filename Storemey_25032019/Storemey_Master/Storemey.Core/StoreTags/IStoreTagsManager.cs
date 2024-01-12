using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTags
{
    public interface IStoreTagsManager : IDomainService
    {
        Task<IEnumerable<StoreTags>> ListAll();

        Task<IQueryable<StoreTags>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreTags input);

        Task Update(StoreTags input);

        Task Delete(StoreTags input);

        Task <StoreTags> GetByID(long ID);
        
    }
}