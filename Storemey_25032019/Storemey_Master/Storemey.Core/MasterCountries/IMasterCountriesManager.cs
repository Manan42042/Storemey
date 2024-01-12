using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.MasterCountries
{
    public interface IMasterCountriesManager : IDomainService
    {
        Task<IEnumerable<MasterCountries>> ListAll();

        Task<IQueryable<MasterCountries>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        Task<int?> GetRecordCount();

        Task Create(MasterCountries input);

        Task CreateOrUpdate(MasterCountries input);

        Task Update(MasterCountries input);

        Task Delete(MasterCountries input);

        Task <MasterCountries> GetByID(long ID);
        
    }
}