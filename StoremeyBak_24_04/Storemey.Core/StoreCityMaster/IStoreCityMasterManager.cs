using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCityMaster
{
    public interface IStoreCityMasterManager : IDomainService
    {
        
        Task<IEnumerable<StoreCityMaster>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task<IEnumerable<StoreCityMaster>> ListAllByStateId(long StateId);

        Task Create(StoreCityMaster input);

        Task Update(StoreCityMaster input);

        Task Delete(StoreCityMaster input);

        Task <StoreCityMaster> GetByID(long ID);
        
    }
}