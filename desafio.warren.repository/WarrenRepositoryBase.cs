using desafio.warren.data.Context;
using desafio.warren.domain.core.Abstracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace desafio.warren.repository
{
    public class WarrenRepositoryBase<TEntity> : IWarrenRepositoryBase<TEntity>, IDisposable where TEntity : class
    {
        #region Variáveis
        private readonly WarrenContext context;
        #endregion

        #region Construtor
        public WarrenRepositoryBase(WarrenContext context)
        {
            this.context = context;
        } 
        #endregion

        public void Dispose()
        {
            context.Dispose();
        }

        public virtual List<TEntity> Listar()
        {
            var entidade = from e in context.Set<TEntity>()
                           .OrderBy(x => (true))
                           select e;

            return entidade.ToList();
        }

        public virtual TEntity Obter(int id)
        {
            var entidade = context.Set<TEntity>().Find(id);

            return entidade;
        }

        public virtual void Inserir(TEntity entidade)
        {
            try
            {
                context.Set<TEntity>().Add(entidade);
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual void Atualizar(TEntity entidade)
        {
            try
            {
                context.Entry(entidade).State = EntityState.Modified;
                context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Excluir(int id)
        {
            try
            {
                TEntity entidade = Obter(id);

                context.Set<TEntity>().Remove(entidade);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void DetachedLocal(Func<TEntity, bool> entidade)
        {
            var local = context.Set<TEntity>().Local.Where(entidade).FirstOrDefault();

            if (local != null)
            {
                context.Entry(local).State = EntityState.Detached;
            }
        }
    }
}