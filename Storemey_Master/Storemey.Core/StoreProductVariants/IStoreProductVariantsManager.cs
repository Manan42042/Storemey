using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductVariants
{
    public interface IStoreProductVariantsManager : IDomainService
    {
        Task<IEnumerable<StoreProductVariants>> ListAll();

        Task<Tuple<IQueryable<StoreProductVariants>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductVariants input);

        Task Update(StoreProductVariants input);

        Task Delete(StoreProductVariants input);

        Task <StoreProductVariants> GetByID(long ID);
        
    }
}