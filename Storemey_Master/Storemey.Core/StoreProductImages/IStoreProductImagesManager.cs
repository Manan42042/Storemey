using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductImages
{
    public interface IStoreProductImagesManager : IDomainService
    {
        Task<IEnumerable<StoreProductImages>> ListAll();

        Task<Tuple<IQueryable<StoreProductImages>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        Task<List<StoreProductImages>> ListAllByProduct(long ProductId);

        Task Create(StoreProductImages input);

        Task Update(StoreProductImages input);

        Task Delete(StoreProductImages input);
        Task DeleteByProductId(long ProductId);

        Task <StoreProductImages> GetByID(long ID);
        
    }
}