using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System;

namespace desafio.warren.services.Services
{
    public class OperacaoService : WarrenServiceBase<Operacao>, IOperacaoService
    {
        #region Variáveis
        private readonly IOperacaoRepository repositoryOperacao;
        #endregion

        #region Construtor
        public OperacaoService(IOperacaoRepository repositoryOperacao) : base(repositoryOperacao)
        {
            this.repositoryOperacao = repositoryOperacao;
        }
        #endregion

        public override Operacao Obter(int idOperacao)
        {
            var operacao = repositoryOperacao.Obter(idOperacao);

            if (operacao == null)
            {
                throw new ApplicationException("Operação Inválida.");
            }

            return operacao;
        }
    }
}