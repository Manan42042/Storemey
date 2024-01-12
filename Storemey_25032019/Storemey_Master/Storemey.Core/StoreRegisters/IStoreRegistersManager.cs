using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreRegisters
{
    public interface IStoreRegistersManager : IDomainService
    {
        Task<IEnumerable<StoreRegisters>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreRegisters input);

        Task CreateOrUpdate(StoreRegisters input);

        Task Update(StoreRegisters input);

        Task Delete(StoreRegisters input);

        Task<StoreRegisters> GetByID(long ID);

    }
}