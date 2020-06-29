using desafio.warren.data.Context;
using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;

namespace desafio.warren.repository
{
    public class MovimentoRepository : WarrenRepositoryBase<Movimento>, IMovimentoRepository
    {
        #region Variáveis
        private readonly WarrenContext context;
        #endregion

        #region Construtor
        public MovimentoRepository(WarrenContext context) : base(context)
        {
            this.context = context;
        } 
        #endregion
    }
}