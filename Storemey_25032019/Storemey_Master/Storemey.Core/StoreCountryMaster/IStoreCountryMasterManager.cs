using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCountryMaster
{
    public interface IStoreCountryMasterManager : IDomainService
    {
        Task<IEnumerable<StoreCountryMaster>> ListAll();

        Task<IQueryable<StoreCountryMaster>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreCountryMaster input);

        Task Update(StoreCountryMaster input);

        Task Delete(StoreCountryMaster input);

        Task <StoreCountryMaster> GetByID(long ID);
        
    }
}