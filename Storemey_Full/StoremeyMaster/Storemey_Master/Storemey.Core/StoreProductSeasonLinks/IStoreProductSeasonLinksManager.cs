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

        Task<Tuple<IQueryable<StoreProductSeasonLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductSeasonLinks input);

        Task Update(StoreProductSeasonLinks input);

        Task Delete(StoreProductSeasonLinks input);

        Task <StoreProductSeasonLinks> GetByID(long ID);

        Task<List<StoreProductSeasonLinks>> GetByProductId(long ID);

        Task DeleteByProductId(long ProductId);

    }
}