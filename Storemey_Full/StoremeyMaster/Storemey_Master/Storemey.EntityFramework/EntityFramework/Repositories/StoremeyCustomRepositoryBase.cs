using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Storemey.EntityFramework.Repositories
{
    //Base class for all repositories in my application
    public class SimpleTaskSystemRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<SimpleTaskSystemDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public SimpleTaskSystemRepositoryBase(IDbContextProvider<SimpleTaskSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //add common methods for all repositories
    }

    //A shortcut for entities those have integer Id
    public class SimpleTaskSystemRepositoryBase<TEntity> : SimpleTaskSystemRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public SimpleTaskSystemRepositoryBase(IDbContextProvider<SimpleTaskSystemDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        //do not add methods here, add them to the class above (because this class inherits it)
    }
}
