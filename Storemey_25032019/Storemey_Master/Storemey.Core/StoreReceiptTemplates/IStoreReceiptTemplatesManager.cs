using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreReceiptTemplates
{
    public interface IStoreReceiptTemplatesManager : IDomainService
    {
        Task<IEnumerable<StoreReceiptTemplates>> ListAll();

        Task<IQueryable<StoreReceiptTemplates>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreReceiptTemplates input);

        Task Update(StoreReceiptTemplates input);

        Task Delete(StoreReceiptTemplates input);

        Task <StoreReceiptTemplates> GetByID(long ID);
        
    }
}