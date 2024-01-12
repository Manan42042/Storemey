using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StorePaymentTypes
{
    public interface IStorePaymentTypesManager : IDomainService
    {
        Task<IEnumerable<StorePaymentTypes>> ListAll();

        Task<IQueryable<StorePaymentTypes>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StorePaymentTypes input);

        Task Update(StorePaymentTypes input);

        Task Delete(StorePaymentTypes input);

        Task <StorePaymentTypes> GetByID(long ID);
        
    }
}