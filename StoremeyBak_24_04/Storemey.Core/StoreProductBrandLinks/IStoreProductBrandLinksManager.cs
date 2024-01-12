using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductBrandLinks
{
    public interface IStoreProductBrandLinksManager : IDomainService
    {
        Task<IEnumerable<StoreProductBrandLinks>> ListAll();

        Task<Tuple<IQueryable<StoreProductBrandLinks>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductBrandLinks input);

        Task Update(StoreProductBrandLinks input);

        Task Delete(StoreProductBrandLinks input);

        Task <StoreProductBrandLinks> GetByID(long ID);
        
    }
}