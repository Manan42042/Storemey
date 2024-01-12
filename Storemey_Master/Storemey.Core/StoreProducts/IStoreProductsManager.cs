using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProducts
{
    public interface IStoreProductsManager : IDomainService
    {
        Task<IEnumerable<StoreProducts>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<List<StoreProducts>> GetVariantsByID(long ID);

        Task<long> Create(StoreProducts input);

        Task<long> Update(StoreProducts input);

        Task Delete(StoreProducts input);

        Task DeleteByProductId(long ProductId);

        Task <StoreProducts> GetByID(long ID);

        Task <List<StoreProducts>> GetVariantsByProductId(long ID);


    }
}