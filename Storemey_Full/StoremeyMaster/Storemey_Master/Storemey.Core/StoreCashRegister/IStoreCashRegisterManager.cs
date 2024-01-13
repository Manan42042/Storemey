using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCashRegister
{
    public interface IStoreCashRegisterManager : IDomainService
    {
        Task<IEnumerable<StoreCashRegister>> ListAll();

        Task<Tuple<IQueryable<StoreCashRegister>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreCashRegister input);

        Task Update(StoreCashRegister input);

        Task Delete(StoreCashRegister input);

        Task <StoreCashRegister> GetByID(long ID);
        
    }
}