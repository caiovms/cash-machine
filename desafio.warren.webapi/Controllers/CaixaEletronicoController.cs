using desafio.warren.application.Abstracts;
using desafio.warren.webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;


namespace desafio.warren.webapi.Controllers
{
    public class CaixaEletronicoController : Controller
    {
        #region Variáveis
        private readonly IMovimentoApplicationService applicationServiceMovimento;
        private readonly IContaApplicationService applicationServiceConta;
        private readonly IOperacaoApplicationService applicationOperacao;

        private static readonly decimal taxaRentabilidade = 0.01M;
        private static CaixaEletronicoViewModel caixaEletronico = new CaixaEletronicoViewModel();
        private readonly ExtratoViewModel extrato = new ExtratoViewModel();
        #endregion

        #region Construtor
        public CaixaEletronicoController(IMovimentoApplicationService applicationServiceMovimento, 
                                         IContaApplicationService applicationServiceConta, 
                                         IOperacaoApplicationService applicationOperacao)
        {
            this.applicationServiceMovimento = applicationServiceMovimento;
            this.applicationServiceConta = applicationServiceConta;
            this.applicationOperacao = applicationOperacao;
        }
        #endregion

        public IActionResult Index()
        {
            InicializarCaixaEletronico();

            if (TempData["MensagemErro"] != null)
            {
                ViewBag.MensagemErro = TempData["MensagemErro"].ToString();
            }

            return View(caixaEletronico);
        }

        [HttpPost]
        public IActionResult Saque(SaqueViewModel saque)
        {
            try
            {
                applicationServiceMovimento.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Index", "CaixaEletronico");
        }

        [HttpPost]
        public IActionResult Deposito(DepositoViewModel transacao)
        {
            try
            {
                applicationServiceMovimento.Deposito(transacao.IdConta, transacao.IdOperacao, transacao.ValorOperacao);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Index", "CaixaEletronico");
        }

        [HttpPost]
        public IActionResult Pagamento(PagamentoViewModel pagamento)
        {
            try
            {
                applicationServiceMovimento.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Index", "CaixaEletronico");
        }

        [HttpPost]
        public IActionResult Rentabilizacao(RentabilizacaoViewModel rentabilizacao)
        {
            try
            {
                applicationServiceMovimento.Rentabilizacao(rentabilizacao.IdConta, rentabilizacao.IdOperacao, taxaRentabilidade);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Index", "CaixaEletronico");
        }

        public IActionResult SetOperacao(int idOperacao)
        {
            try
            {
                caixaEletronico.IdOperacao = applicationOperacao.Obter(idOperacao).Id;
                caixaEletronico.CodigoDeBarras = null;
                caixaEletronico.ValorOperacao = null;
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return RedirectToAction("Index", "CaixaEletronico");
        }
        
        private void InicializarCaixaEletronico()
        {
            try
            {
                var conta = applicationServiceConta.Obter(1);

                caixaEletronico.Conta = conta;
                caixaEletronico.Extrato = extrato;
                caixaEletronico.Extrato.Movimentos = conta.Movimentos.ToList();
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
        }
    }
}