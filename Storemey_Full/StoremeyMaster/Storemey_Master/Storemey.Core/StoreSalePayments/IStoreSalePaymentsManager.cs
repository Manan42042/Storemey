using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreSalePayments
{
    public interface IStoreSalePaymentsManager : IDomainService
    {
        Task<IEnumerable<StoreSalePayments>> ListAll();

        Task<Tuple<IQueryable<StoreSalePayments>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreSalePayments input);

        Task Update(StoreSalePayments input);

        Task Delete(StoreSalePayments input);

        Task <StoreSalePayments> GetByID(long ID);
        
    }
}