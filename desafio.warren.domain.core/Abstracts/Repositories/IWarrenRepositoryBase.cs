using System;
using System.Collections.Generic;

namespace desafio.warren.domain.core.Abstracts.Repositories
{
    public interface IWarrenRepositoryBase<TEntity> where TEntity : class
    {
        List<TEntity> Listar();

        TEntity Obter(int id);

        void Inserir(TEntity entity);

        void Atualizar(TEntity entity);

        void Excluir(int id);
    }
}