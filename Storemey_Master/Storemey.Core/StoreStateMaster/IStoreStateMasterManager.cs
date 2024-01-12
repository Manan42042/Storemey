using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreStateMaster
{
    public interface IStoreStateMasterManager : IDomainService
    {
        Task<IEnumerable<StoreStateMaster>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task<IEnumerable<StoreStateMaster>> ListAllByCountryID(long Id);

        Task Create(StoreStateMaster input);

        Task Update(StoreStateMaster input);

        Task Delete(StoreStateMaster input);

        Task <StoreStateMaster> GetByID(long ID);
        
    }
}