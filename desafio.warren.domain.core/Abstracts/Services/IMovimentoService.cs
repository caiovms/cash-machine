using desafio.warren.domain.Entities;
using System;

namespace desafio.warren.domain.core.Abstracts.Services
{
    public interface IMovimentoService : IWarrenServiceBase<Movimento>
    {
        void Saque(int idConta, int idOperacao, decimal valor);
        void Deposito(int idConta, int idOperacao,  decimal valor);
        void Pagamento(int idConta, int idOperacao, decimal valor, string codigoBarras);
        void Rentabilizacao(int idConta, int idOperacao, decimal taxa);
    }
}
