using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTaxGroups
{
    public interface IStoreTaxGroupsManager : IDomainService
    {
        Task<IEnumerable<StoreTaxGroups>> ListAll();

        Task<Tuple<IQueryable<StoreTaxGroups>, int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        Task Create(StoreTaxGroups input);

        Task Update(StoreTaxGroups input);

        Task Delete(StoreTaxGroups input);

        Task<StoreTaxGroups> GetByID(long ID);


    }
}