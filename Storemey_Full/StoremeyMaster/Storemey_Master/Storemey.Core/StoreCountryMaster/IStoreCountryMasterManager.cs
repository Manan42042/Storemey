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

        Task<Tuple<IQueryable<StoreCountryMaster>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreCountryMaster input);

        Task Update(StoreCountryMaster input);

        Task Delete(StoreCountryMaster input);

        Task <StoreCountryMaster> GetByID(long ID);
        
    }
}