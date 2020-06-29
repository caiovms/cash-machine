using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System;

namespace desafio.warren.services.Services
{
    public class MovimentoService : WarrenServiceBase<Movimento>, IMovimentoService
    {
        #region Variáveis
        private readonly IMovimentoRepository repositoryMovimento;
        private readonly IContaRepository repositoryConta;
        private readonly IOperacaoRepository repositoryOperacao;
        #endregion

        #region Construtor
        public MovimentoService(IOperacaoRepository repositoryOperacao, IContaRepository repositoryConta, IMovimentoRepository repositoryMovimento) : base(repositoryMovimento)
        {
            this.repositoryMovimento = repositoryMovimento;
            this.repositoryConta = repositoryConta;
            this.repositoryOperacao = repositoryOperacao;
        }
        #endregion

        public void Saque(int idConta, int idOperacao, decimal valor)
        {
            var conta = repositoryConta.Obter(idConta);

            ValidarOperacao(conta, idOperacao, valor, null);

            GerarMovimento(conta, valor, idOperacao, null);
        }

        public void Deposito(int idConta, int idOperacao, decimal valor)
        {
            var conta = repositoryConta.Obter(idConta);

            ValidarOperacao(conta, idOperacao, valor, null);

            GerarMovimento(conta, valor, idOperacao, null);
        }

        public void Pagamento(int idConta, int idOperacao, decimal valor, string codigoBarras)
        {
            var conta = repositoryConta.Obter(idConta);

            ValidarOperacao(conta, idOperacao, valor, codigoBarras);

            GerarMovimento(conta, valor, idOperacao, codigoBarras);
        }

        public void Rentabilizacao(int idConta, int idOperacao, decimal taxa)
        {
            var conta = repositoryConta.Obter(idConta);

            ValidarOperacao(conta, idOperacao, null, null);

            if (conta.Saldo > 0)
            {
                var rendimento = conta.Saldo * taxa;

                GerarMovimento(conta, rendimento, idOperacao, null);
            }
        }

        private void ValidarOperacao(Conta conta, int idOperacao, decimal? valor, string codigoBarras)
        {
            var operacao = repositoryOperacao.Obter(idOperacao);

            if (conta == null)
            {
                throw new ApplicationException("Conta Inválida.");
            }

            if (operacao == null || valor <= 0)
            {
                throw new ApplicationException("Operação Inválida.");
            }

            if (operacao.Id == (byte)TipoOperacao.PAGAMENTO && (string.IsNullOrEmpty(codigoBarras) || codigoBarras.Length > 48))
            {
                throw new ApplicationException("Código de Barras Inválido.");
            }

            if ((operacao.Id == (byte)TipoOperacao.SAQUE || operacao.Id == (byte)TipoOperacao.PAGAMENTO) && conta.Saldo < valor)
            {
                throw new ApplicationException("Saldo Insuficiente.");
            }
        }

        private void GerarMovimento(Conta conta, decimal valor, int idOperacao, string codigoBarras)
        {
            try
            {
                var valorMovimento = AtualizarSaldo(conta, valor, idOperacao);

                Movimento movimento = new Movimento
                {
                    IdConta = conta.Id,
                    IdOperacao = idOperacao,
                    Valor = valorMovimento,
                    Data = DateTime.Now,
                    CodigoBarras = codigoBarras
                };

                repositoryMovimento.Inserir(movimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal AtualizarSaldo(Conta conta, decimal valor, int idOperacao)
        {
            switch (idOperacao)
            {
                case (int)TipoOperacao.SAQUE:
                case (int)TipoOperacao.PAGAMENTO:
                    conta.Saldo = conta.Saldo - valor;
                    valor = -Math.Abs(valor);
                    break;

                case (int)TipoOperacao.RENTABILIZACAO:
                case (int)TipoOperacao.DEPOSITO:
                    conta.Saldo = conta.Saldo + valor;
                    break;
            }

            repositoryConta.Atualizar(conta);

            return valor;
        }
    }
}