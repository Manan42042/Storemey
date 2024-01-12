using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreGiftCards
{
    public interface IStoreGiftCardsManager : IDomainService
    {
        Task<IEnumerable<StoreGiftCards>> ListAll();

        Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        Task Create(StoreGiftCards input);

        Task Update(StoreGiftCards input);

        Task Delete(StoreGiftCards input);

        Task <StoreGiftCards> GetByID(long ID);
        
    }
}