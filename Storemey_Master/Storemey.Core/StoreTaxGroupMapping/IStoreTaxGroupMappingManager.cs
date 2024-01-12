using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace Storemey.StoreTaxGroupLinks
{
    public interface IStoreTaxGroupLinksManager : IDomainService
    {
        Task<IEnumerable<StoreTaxGroupLinks>> ListAll();

        Task Create(StoreTaxGroupLinks input);

        Task Update(StoreTaxGroupLinks input);

        Task Delete(StoreTaxGroupLinks input);

        Task<StoreTaxGroupLinks> GetByID(long ID);

        Task<IEnumerable<StoreTaxGroupLinks>> GetByGroupID(int groupId);
        Task DeleteByGroupID(int groupId);

    }
}