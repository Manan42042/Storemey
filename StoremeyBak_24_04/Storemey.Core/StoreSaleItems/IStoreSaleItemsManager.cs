using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreSaleItems
{
    public interface IStoreSaleItemsManager : IDomainService
    {
        Task<IEnumerable<StoreSaleItems>> ListAll();

        Task<Tuple<IQueryable<StoreSaleItems>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreSaleItems input);

        Task Update(StoreSaleItems input);

        Task Delete(StoreSaleItems input);

        Task <StoreSaleItems> GetByID(long ID);
        
    }
}