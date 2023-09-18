using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cash.Machine.Repository
{
    public class BaseRepository<TEntity> : IRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        private readonly DataContext _dataContext;

        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        } 

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        public virtual List<TEntity> List()
        {
            return _dataContext.Set<TEntity>().ToList();
        }

        public virtual TEntity Get(int id)
        {
            return _dataContext.Set<TEntity>().Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            try
            {
                _dataContext.Set<TEntity>().Add(entity);
                _dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual void Update(TEntity entity)
        {
            try
            {
                _dataContext.Entry(entity).State = EntityState.Modified;
                _dataContext.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(int id)
        {
            try
            {
                TEntity entity = Get(id);

                _dataContext.Set<TEntity>().Remove(entity);
                _dataContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void DetachedLocal(Func<TEntity, bool> entity)
        {
            var local = _dataContext.Set<TEntity>().Local.Where(entity).FirstOrDefault();

            if (local != null)
            {
                _dataContext.Entry(local).State = EntityState.Detached;
            }
        }
    }
}