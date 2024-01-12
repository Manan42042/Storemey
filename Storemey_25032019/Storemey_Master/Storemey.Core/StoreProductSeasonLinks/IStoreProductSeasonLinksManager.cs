using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductSeasonLinks
{
    public interface IStoreProductSeasonLinksManager : IDomainService
    {
        Task<IEnumerable<StoreProductSeasonLinks>> ListAll();

        Task<IQueryable<StoreProductSeasonLinks>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(StoreProductSeasonLinks input);

        Task Update(StoreProductSeasonLinks input);

        Task Delete(StoreProductSeasonLinks input);

        Task <StoreProductSeasonLinks> GetByID(long ID);
        
    }
}