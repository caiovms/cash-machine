using System;
using System.Collections.Generic;

namespace desafio.warren.domain.core.Abstracts.Services
{
    public interface IWarrenServiceBase<TEntity> where TEntity : class
    {
        List<TEntity> Listar();

        TEntity Obter(int id);

        void Inserir(TEntity entidade);

        void Atualizar(TEntity entidade);

        void Excluir(int id);
    }
}