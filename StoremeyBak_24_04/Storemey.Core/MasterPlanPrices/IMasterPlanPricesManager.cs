using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.MasterPlanPrices
{
    public interface IMasterPlanPricesManager : IDomainService
    {
        Task<IEnumerable<MasterPlanPrices>> ListAll();

        Task<IQueryable<MasterPlanPrices>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        

        Task Create(MasterPlanPrices input);

        Task Update(MasterPlanPrices input);

        Task Delete(MasterPlanPrices input);

        Task CreateOrUpdate(MasterPlanPrices input);

        Task <MasterPlanPrices> GetByID(long ID);

        MasterPlanPrices GetByPlanIDAndCountryID(long countryId,long planID);

    }
}