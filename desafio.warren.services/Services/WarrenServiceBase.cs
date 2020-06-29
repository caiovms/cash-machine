using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.core.Abstracts.Services;
using System;
using System.Collections.Generic;

namespace desafio.warren.services.Services
{
    public class WarrenServiceBase<TEntity> : IWarrenServiceBase<TEntity> where TEntity : class
    {
        #region Variáveis
        private readonly IWarrenRepositoryBase<TEntity> repositoryBase;
        #endregion

        #region Construtor
        public WarrenServiceBase(IWarrenRepositoryBase<TEntity> repositoryBase)
        {
            this.repositoryBase = repositoryBase;
        } 
        #endregion

        public virtual List<TEntity> Listar()
        {
            return repositoryBase.Listar();
        }

        public virtual TEntity Obter(int id)
        {
            return repositoryBase.Obter(id);
        }

        public virtual void Inserir(TEntity entidade)
        {
            repositoryBase.Inserir(entidade);
        }

        public virtual void Atualizar(TEntity entidade)
        {
            repositoryBase.Atualizar(entidade);
        }

        public virtual void Excluir(int id)
        {
            repositoryBase.Excluir(id);
        }
    }
}