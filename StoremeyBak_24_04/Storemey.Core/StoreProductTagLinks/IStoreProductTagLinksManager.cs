using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductTagLinks
{
    public interface IStoreProductTagLinksManager : IDomainService
    {
        Task<IEnumerable<StoreProductTagLinks>> ListAll();

        Task<Tuple<IQueryable<StoreProductTagLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductTagLinks input);

        Task Update(StoreProductTagLinks input);

        Task Delete(StoreProductTagLinks input);

        Task <StoreProductTagLinks> GetByID(long ID);
        
    }
}