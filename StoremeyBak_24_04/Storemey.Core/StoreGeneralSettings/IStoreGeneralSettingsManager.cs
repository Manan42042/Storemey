using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreGeneralSettings
{
    public interface IStoreGeneralSettingsManager : IDomainService
    {
        Task<IEnumerable<StoreGeneralSettings>> ListAll();

        Task<Tuple<IQueryable<StoreGeneralSettings>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreGeneralSettings input);

        Task Update(StoreGeneralSettings input);

        Task Delete(StoreGeneralSettings input);

        Task <StoreGeneralSettings> GetByID(long ID);
        
    }
}