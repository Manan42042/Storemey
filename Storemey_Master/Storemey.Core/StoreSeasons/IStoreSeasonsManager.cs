using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreSeasons
{
    public interface IStoreSeasonsManager : IDomainService
    {
        Task<IEnumerable<StoreSeasons>> ListAll();

        Task<Tuple<IQueryable<StoreSeasons>,int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreSeasons input);

        Task Update(StoreSeasons input);

        Task Delete(StoreSeasons input);

        Task <StoreSeasons> GetByID(long ID);
        
    }
}