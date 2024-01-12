using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.MasterPlans
{
    public interface IMasterPlansManager : IDomainService
    {
        Task<IEnumerable<MasterPlans>> ListAll();

        Task<IQueryable<MasterPlans>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);


        

        Task Create(MasterPlans input);

        Task CreateOrUpdate(MasterPlans input);

        Task Update(MasterPlans input);

        Task Delete(MasterPlans input);

        Task <MasterPlans> GetByID(long ID);
        
    }
}