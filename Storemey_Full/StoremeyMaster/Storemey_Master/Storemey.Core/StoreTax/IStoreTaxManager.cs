using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTax
{
    public interface IStoreTaxManager : IDomainService
    {
        Task<IEnumerable<StoreTax>> ListAll();

        Task<Tuple<IQueryable<StoreTax>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);
        
        Task Create(StoreTax input);

        Task Update(StoreTax input);

        Task Delete(StoreTax input);

        Task <StoreTax> GetByID(long ID);
        
    }
}