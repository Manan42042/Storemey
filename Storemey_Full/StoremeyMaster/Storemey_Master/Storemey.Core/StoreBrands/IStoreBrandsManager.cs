using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreBrands
{
    public interface IStoreBrandsManager : IDomainService
    {
        Task<IEnumerable<StoreBrands>> ListAll();

        Task<Tuple<IQueryable<StoreBrands>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreBrands input);

        Task Update(StoreBrands input);

        Task Delete(StoreBrands input);

        Task <StoreBrands> GetByID(long ID);
        
    }
}