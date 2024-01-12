using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreSaleTransactions
{
    public interface IStoreSaleTransactionsManager : IDomainService
    {
        Task<IEnumerable<StoreSaleTransactions>> ListAll();

        Task<Tuple<IQueryable<StoreSaleTransactions>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreSaleTransactions input);

        Task Update(StoreSaleTransactions input);

        Task Delete(StoreSaleTransactions input);

        Task <StoreSaleTransactions> GetByID(long ID);
        
    }
}