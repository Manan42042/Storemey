using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreCurrencies
{
    public interface IStoreCurrenciesManager : IDomainService
    {
        Task<IEnumerable<StoreCurrencies>> ListAll();

        Task<Tuple<IQueryable<StoreCurrencies>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreCurrencies input);

        Task CreateOrUpdate(StoreCurrencies input);

        Task Update(StoreCurrencies input);

        Task Delete(StoreCurrencies input);

        Task <StoreCurrencies> GetByID(long ID);
        
    }
}