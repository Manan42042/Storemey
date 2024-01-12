using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Services;


namespace Storemey.Authorization.Users
{
    public interface IUserManager : IDomainService
    {
        Task Create(User input);

        Task Update(User input);

        Task Delete(User input);

        Task <User> GetByID(long ID);
        
    }
}