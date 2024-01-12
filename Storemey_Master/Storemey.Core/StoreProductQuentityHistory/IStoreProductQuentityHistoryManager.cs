using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreProductQuentityHistory
{
    public interface IStoreProductQuentityHistoryManager : IDomainService
    {
        Task<IEnumerable<StoreProductQuentityHistory>> ListAll();

        Task<Tuple<IQueryable<StoreProductQuentityHistory>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        Task<List<StoreProductQuentityHistory>> ListAllProductAndOutlet(long ProductId, long OutletId);

        Task Create(StoreProductQuentityHistory input);

        Task Update(StoreProductQuentityHistory input);

        Task Delete(StoreProductQuentityHistory input);

        Task <StoreProductQuentityHistory> GetByID(long ID);
        
    }
}