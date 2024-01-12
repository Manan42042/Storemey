using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace Storemey.EntityFramework.Repositories
{
    public abstract class StoremeyRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<StoremeyDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected StoremeyRepositoryBase(IDbContextProvider<StoremeyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class StoremeyRepositoryBase<TEntity> : StoremeyRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected StoremeyRepositoryBase(IDbContextProvider<StoremeyDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
