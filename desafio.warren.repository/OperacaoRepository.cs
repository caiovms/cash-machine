using desafio.warren.data.Context;
using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;

namespace desafio.warren.repository
{
    public class OperacaoRepository : WarrenRepositoryBase<Operacao>, IOperacaoRepository
    {
        #region Variáveis
        private readonly WarrenContext context;
        #endregion

        #region Construtor
        public OperacaoRepository(WarrenContext context) : base(context)
        {
            this.context = context;
        }
        #endregion
    }
}