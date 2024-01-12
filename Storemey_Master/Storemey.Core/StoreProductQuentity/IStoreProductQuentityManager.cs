using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductQuentity
{
    public interface IStoreProductQuentityManager : IDomainService
    {
        Task<IEnumerable<StoreProductQuentity>> ListAll();

        Task<List<StoreProductQuentity>> ListAllProductAndOutlet(long ProductId, long OutletId);

        Task<Tuple<IQueryable<StoreProductQuentity>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreProductQuentity input);

        Task Update(StoreProductQuentity input);

        Task Delete(StoreProductQuentity input);

        Task <StoreProductQuentity> GetByID(long ID);
        
    }
}