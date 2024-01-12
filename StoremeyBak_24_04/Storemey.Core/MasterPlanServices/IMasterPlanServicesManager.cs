using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.MasterPlanServices
{
    public interface IMasterPlanServicesManager : IDomainService
    {
        Task<IEnumerable<MasterPlanServices>> ListAll();

        Task<IQueryable<MasterPlanServices>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection);

        

        List<MasterPlanServices> GetByPlanID(long ID);

        Task Create(MasterPlanServices input);

        Task CreateOrUpdate(MasterPlanServices input);

        Task Update(MasterPlanServices input);

        Task Delete(MasterPlanServices input);

        Task <MasterPlanServices> GetByID(long ID);
        
    }
}