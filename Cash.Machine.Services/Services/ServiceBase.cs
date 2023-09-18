using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Core.Abstracts.Services;
using System.Collections.Generic;

namespace Cash.Machine.Services.Services
{
    public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _baseRepository;

        public ServiceBase(IRepositoryBase<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        } 

        public virtual List<TEntity> List()
        {
            return _baseRepository.List();
        }

        public virtual TEntity Get(int id)
        {
            return _baseRepository.Get(id);
        }

        public virtual void Add(TEntity entidade)
        {
            _baseRepository.Add(entidade);
        }

        public virtual void Update(TEntity entidade)
        {
            _baseRepository.Update(entidade);
        }

        public virtual void Delete(int id)
        {
            _baseRepository.Delete(id);
        }
    }
}