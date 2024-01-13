using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductVariantValues
{
    public interface IStoreProductVariantValuesManager : IDomainService
    {
        Task<IEnumerable<StoreProductVariantValues>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductVariantValues input);

        Task Update(StoreProductVariantValues input);

        Task Delete(StoreProductVariantValues input);

        Task <StoreProductVariantValues> GetByID(long ID);
        
    }
}