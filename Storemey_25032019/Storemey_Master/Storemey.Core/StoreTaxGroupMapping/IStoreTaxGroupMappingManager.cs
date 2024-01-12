using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTaxGroupMapping
{
    public interface IStoreTaxGroupMappingManager : IDomainService
    {
        Task<IEnumerable<StoreTaxGroupMapping>> ListAll();

        Task Create(StoreTaxGroupMapping input);

        Task Update(StoreTaxGroupMapping input);

        Task Delete(StoreTaxGroupMapping input);

        Task <StoreTaxGroupMapping> GetByID(long ID);
        
    }
}