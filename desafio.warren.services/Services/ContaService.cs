using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System;

namespace desafio.warren.services.Services
{
    public class ContaService : WarrenServiceBase<Conta>, IContaService
    {
        #region Variáveis
        private readonly IContaRepository repositoryConta;
        #endregion

        #region Construtor
        public ContaService(IContaRepository repositoryConta) : base(repositoryConta)
        {
            this.repositoryConta = repositoryConta;
        }
        #endregion

        public override Conta Obter(int idConta)
        {
            var conta = repositoryConta.Obter(idConta);

            if (conta == null)
            {
                throw new ApplicationException("Conta Inválida.");
            }

            return conta;
        }
    }
}