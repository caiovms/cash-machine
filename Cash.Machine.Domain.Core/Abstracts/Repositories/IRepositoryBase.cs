using System.Collections.Generic;

namespace Cash.Machine.Domain.Core.Abstracts.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        List<TEntity> List();

        TEntity Get(int id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}