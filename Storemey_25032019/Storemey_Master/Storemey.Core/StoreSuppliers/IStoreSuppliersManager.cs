using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreSuppliers
{
    public interface IStoreSuppliersManager : IDomainService
    {
        Task<IEnumerable<StoreSuppliers>> ListAll();

        Task<IQueryable<StoreSuppliers>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreSuppliers input);

        Task Update(StoreSuppliers input);

        Task Delete(StoreSuppliers input);

        Task <StoreSuppliers> GetByID(long ID);
        
    }
}