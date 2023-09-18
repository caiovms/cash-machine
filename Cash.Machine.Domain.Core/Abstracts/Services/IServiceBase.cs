using System.Collections.Generic;

namespace Cash.Machine.Domain.Core.Abstracts.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        List<TEntity> List();

        TEntity Get(int id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}