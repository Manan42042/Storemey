using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCashRegisterInOut
{
    public interface IStoreCashRegisterInOutManager : IDomainService
    {
        Task<IEnumerable<StoreCashRegisterInOut>> ListAll();

        Task<Tuple<IQueryable<StoreCashRegisterInOut>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreCashRegisterInOut input);

        Task Update(StoreCashRegisterInOut input);

        Task Delete(StoreCashRegisterInOut input);

        Task <StoreCashRegisterInOut> GetByID(long ID);
        
    }
}