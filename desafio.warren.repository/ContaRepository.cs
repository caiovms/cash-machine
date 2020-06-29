using desafio.warren.data.Context;
using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace desafio.warren.repository
{
    public class ContaRepository : WarrenRepositoryBase<Conta>, IContaRepository
    {
        #region Variáveis
        private readonly WarrenContext context;
        #endregion

        #region Construtor
        public ContaRepository(WarrenContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        public override List<Conta> Listar()
        {
            var consulta = (from conta in context.Contas
                            .Include(c => c.Movimentos)
                            select conta).ToList();

            return consulta;
        }

        public override Conta Obter(int id)
        {
            var consulta = (from conta in context.Contas
                            .Include(c => c.Movimentos)
                            select conta).SingleOrDefault();

            return consulta;
        }
    }
}