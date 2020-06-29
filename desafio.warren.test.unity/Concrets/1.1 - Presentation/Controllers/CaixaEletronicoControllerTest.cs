using desafio.warren.application.Abstracts;
using desafio.warren.test.unity.DataTest.Fixtures;
using desafio.warren.webapi.Controllers;
using desafio.warren.webapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Presentation.Controllers
{
    public class CaixaEletronicoControllerTest
    {
        #region Variáveis
        private readonly CaixaEletronicoController caixaEletronicoController;
        private readonly Mock<IMovimentoApplicationService> mockApplicationServiceMovimento;
        private readonly Mock<IContaApplicationService> mockApplicationServiceConta;
        private readonly Mock<IOperacaoApplicationService> mockApplicationServiceOperacao;
        private readonly CaixaEletronicoTestsFixture caixaEletronicoTestsFixture;
        private readonly HttpContext httpContext;
        private readonly TempDataDictionary tempData;
        private readonly ContaTestsFixture contaTestsFixture;
        private readonly OperacaoTestFixture operacaoTestFixture;
        #endregion

        #region Construtor
        public CaixaEletronicoControllerTest()
        {
            caixaEletronicoTestsFixture = new CaixaEletronicoTestsFixture();
            contaTestsFixture = new ContaTestsFixture();
            operacaoTestFixture = new OperacaoTestFixture();

            mockApplicationServiceConta = new Mock<IContaApplicationService>();
            mockApplicationServiceMovimento = new Mock<IMovimentoApplicationService>();
            mockApplicationServiceOperacao = new Mock<IOperacaoApplicationService>();

            caixaEletronicoController = new CaixaEletronicoController(mockApplicationServiceMovimento.Object, 
                                                                      mockApplicationServiceConta.Object, 
                                                                      mockApplicationServiceOperacao.Object);
            
            httpContext = new DefaultHttpContext();
            tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            caixaEletronicoController.TempData = tempData;

        }
        #endregion

        #region Index
        [Fact(DisplayName = "Inicializar Caixa Eletrônico com Sucesso")]
        [Trait("Index", "Caixa Eletronico Controller")]
        public void DeveInicializarCaixaEletronicoSucesso()
        {
            //Arrange
            var conta = contaTestsFixture.GerarContasDTO(1).FirstOrDefault();
            mockApplicationServiceConta.Setup(applicationServiceConta => applicationServiceConta.Obter(It.IsAny<int>())).Returns(conta);

            //Act
            IActionResult actual = caixaEletronicoController.Index();
            ViewResult viewResult = actual as ViewResult;

            //Assert
            Assert.NotNull(viewResult);
            Assert.True(viewResult.TempData.Count == 0);
            Assert.True(viewResult.ViewData.ModelState.IsValid);
        }

        [Fact(DisplayName = "Inicializar Caixa Eletrônico com Falha")]
        [Trait("Index", "Caixa Eletronico Controller")]
        public void DeveFalharInicializarCaixaEletronico()
        {
            //Arrange
            mockApplicationServiceConta.Setup(applicationServiceConta => applicationServiceConta.Obter(It.IsAny<int>())).Throws(new Exception("Conta Inválida."));

            //Act
            IActionResult resultado = caixaEletronicoController.Index();
            ViewResult viewResult = resultado as ViewResult;
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceConta.Object.Obter(new int()));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            Assert.True(viewResult.TempData.Count > 0);
        }
        #endregion

        #region SetOperacao
        [Fact(DisplayName = "Setar Operação com Sucesso")]
        [Trait("SetOperacao", "Caixa Eletronico Controller")]
        public void DeveSetarOperacaoSucesso()
        {
            //Arrange
            var idOperacao = operacaoTestFixture.GerarOperacaoValida();

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.SetOperacao(idOperacao);

            //Assert
            Assert.NotNull(resultado);
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
        }

        [Fact(DisplayName = "Setar Operação com Falha")]
        [Trait("SetOperacao", "Caixa Eletronico Controller")]
        public void DeveFalharSetarOperacaoInvalida()
        {
            //Arrange
            var idOperacao = 0;
            mockApplicationServiceOperacao.Setup(applicationOperacao => applicationOperacao.Obter(It.IsAny<int>())).Throws(new Exception("Operação Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.SetOperacao(idOperacao);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceOperacao.Object.Obter(idOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Operação Inválida.", execao.Message);
        }
        #endregion

        #region Saque
        [Fact(DisplayName = "Efetuar Saque com Sucesso")]
        [Trait("Saque", "Caixa Eletronico Controller")]
        public void DeveExecutarSaqueSucesso()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Saque(It.IsAny<int>(),
                                                                                                               It.IsAny<int>(),
                                                                                                               It.IsAny<decimal>()));
            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Saque(saque);


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            mockApplicationServiceMovimento.Verify(applicationServiceMovimento => applicationServiceMovimento.Saque(It.IsAny<int>(),
                                                                                                               It.IsAny<int>(),
                                                                                                               It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Conta Inválida")]
        [Trait("Saque", "Caixa Eletronico Controller")]
        public void DeveFalharSaqueContaInvalida()
        {
            //Arrange
            var saque = new SaqueViewModel();

            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Saque(It.IsAny<int>(),
                                                                                                               It.IsAny<int>(),
                                                                                                               It.IsAny<decimal>()))
                                                                                                              .Throws(new Exception("Conta Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Saque(saque);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Conta Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Saque por Operação Inválida")]
        [Trait("Saque", "Caixa Eletronico Controller")]
        public void DeveFalharSaqueOperacaoInvalida()
        {
            //Arrange
            var saque = new SaqueViewModel();

            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Saque(It.IsAny<int>(),
                                                                                                               It.IsAny<int>(),
                                                                                                               It.IsAny<decimal>()))
                                                                                                              .Throws(new Exception("Operação Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Saque(saque);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Operação Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Saque por Saldo Insuficiente")]
        [Trait("Saque", "Caixa Eletronico Controller")]
        public void DeveFalharSaqueSaldoInsuficiente()
        {
            //Arrange
            var saque = new SaqueViewModel();

            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Saque(It.IsAny<int>(),
                                                                                                               It.IsAny<int>(),
                                                                                                               It.IsAny<decimal>()))
                                                                                                              .Throws(new Exception("Saldo Insuficiente."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Saque(saque);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Saldo Insuficiente.", execao.Message);
        }
        #endregion

        #region Deposito
        [Fact(DisplayName = "Efetuar Depósito com Sucesso")]
        [Trait("Depósito", "Caixa Eletronico Controller")]
        public void DeveExecutarDepositoSucesso()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Deposito(It.IsAny<int>(),
                                                                                                                  It.IsAny<int>(),
                                                                                                                  It.IsAny<decimal>()));
            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Deposito(deposito);


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            mockApplicationServiceMovimento.Verify(applicationServiceMovimento => applicationServiceMovimento.Deposito(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Conta Inválida")]
        [Trait("Depósito", "Caixa Eletronico Controller")]
        public void DeveFalharDepositoContaInvalida()
        {
            //Arrange
            var deposito = new DepositoViewModel();

            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Deposito(It.IsAny<int>(),
                                                                                                                  It.IsAny<int>(),
                                                                                                                  It.IsAny<decimal>()))
                                                                                                                  .Throws(new Exception("Conta Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Deposito(deposito);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Deposito(deposito.IdConta, deposito.IdOperacao, deposito.ValorOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Conta Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Depósito por Operação Inválida")]
        [Trait("Depósito", "Caixa Eletronico Controller")]
        public void DeveFalharDepositoOperacaoInvalida()
        {
            //Arrange
            var deposito = new DepositoViewModel();

            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Deposito(It.IsAny<int>(),
                                                                                                                  It.IsAny<int>(),
                                                                                                                  It.IsAny<decimal>()))
                                                                                                                  .Throws(new Exception("Operação Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Deposito(deposito);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Deposito(deposito.IdConta, deposito.IdOperacao, deposito.ValorOperacao));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Operação Inválida.", execao.Message);
        }
        #endregion

        #region Pagamento
        [Fact(DisplayName = "Efetuar Pagamento com Sucesso")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveExecutarPagamentoSucesso()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>(),
                                                                                                                   It.IsAny<string>()));
            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Pagamento(pagamento);

            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            mockApplicationServiceMovimento.Verify(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                    It.IsAny<int>(),
                                                                                                                    It.IsAny<decimal>(),
                                                                                                                    It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Conta Inválida")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveFalharPagamentoContaInvalida()
        {
            //Arrange
            var pagamento = new PagamentoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>(),
                                                                                                                   It.IsAny<string>()))
                                                                                                                  .Throws(new Exception("Conta Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Pagamento(pagamento);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Pagamento(pagamento.IdConta, 
                                                                                                     pagamento.IdOperacao, 
                                                                                                     pagamento.ValorOperacao, 
                                                                                                     pagamento.CodigoDeBarras));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Conta Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Pagamento por Operação Inválida")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveFalharPagamentoOperacaoInvalida()
        {
            //Arrange
            var pagamento = new PagamentoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>(),
                                                                                                                   It.IsAny<string>()))
                                                                                                                  .Throws(new Exception("Operação Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Pagamento(pagamento);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Pagamento(pagamento.IdConta,
                                                                                                     pagamento.IdOperacao,
                                                                                                     pagamento.ValorOperacao,
                                                                                                     pagamento.CodigoDeBarras));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Operação Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Pagamento por Saldo Insuficiente")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveFalharPagamentoSaldoInsuficiente()
        {
            //Arrange
            var pagamento = new PagamentoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>(),
                                                                                                                   It.IsAny<string>()))
                                                                                                                  .Throws(new Exception("Saldo Insuficiente."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Pagamento(pagamento);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Pagamento(pagamento.IdConta,
                                                                                                     pagamento.IdOperacao,
                                                                                                     pagamento.ValorOperacao,
                                                                                                     pagamento.CodigoDeBarras));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Saldo Insuficiente.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Pagamento por Código de Barras Inválido")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveFalharPagamentoCodigoBarrasInvalido()
        {
            //Arrange
            var pagamento = new PagamentoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Pagamento(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>(),
                                                                                                                   It.IsAny<string>()))
                                                                                                                  .Throws(new Exception("Código de Barras Inválido."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Pagamento(pagamento);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Pagamento(pagamento.IdConta,
                                                                                                     pagamento.IdOperacao,
                                                                                                     pagamento.ValorOperacao,
                                                                                                     pagamento.CodigoDeBarras));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Código de Barras Inválido.", execao.Message);
        }
        #endregion

        #region Rentabilizacao
        [Fact(DisplayName = "Efetuar Pagamento com Sucesso")]
        [Trait("Pagamento", "Caixa Eletronico Controller")]
        public void DeveExecutarRentabilizarSucesso()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                                                        It.IsAny<int>(),
                                                                                                                        It.IsAny<decimal>()));
            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Rentabilizacao(rentabilizacao);

            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            mockApplicationServiceMovimento.Verify(applicationServiceMovimento => applicationServiceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                                                         It.IsAny<int>(),
                                                                                                                         It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Conta Inválida")]
        [Trait("Rentabilização", "Caixa Eletronico Controller")]
        public void DeveFalharRentabilizacaoContaInvalida()
        {
            //Arrange
            var rentabilizacao = new RentabilizacaoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>()))
                                                                                                                  .Throws(new Exception("Conta Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Rentabilizacao(rentabilizacao);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Rentabilizacao(rentabilizacao.IdConta,
                                                                                                          rentabilizacao.IdOperacao,
                                                                                                          rentabilizacao.TaxaRentabilidade));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Conta Inválida.", execao.Message);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Operação Inválida")]
        [Trait("Rentabilização", "Caixa Eletronico Controller")]
        public void DeveFalharRentabilizacaoOperacaoInvalida()
        {
            //Arrange
            var rentabilizacao = new RentabilizacaoViewModel();
            mockApplicationServiceMovimento.Setup(applicationServiceMovimento => applicationServiceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                                                   It.IsAny<int>(),
                                                                                                                   It.IsAny<decimal>()))
                                                                                                                  .Throws(new Exception("Operação Inválida."));

            //Act
            RedirectToActionResult resultado = (RedirectToActionResult)caixaEletronicoController.Rentabilizacao(rentabilizacao);
            var execao = Assert.Throws<Exception>(() => mockApplicationServiceMovimento.Object.Rentabilizacao(rentabilizacao.IdConta,
                                                                                                          rentabilizacao.IdOperacao,
                                                                                                          rentabilizacao.TaxaRentabilidade));


            //Assert
            Assert.Equal("Index", resultado.ActionName);
            Assert.Equal("CaixaEletronico", resultado.ControllerName);
            Assert.Equal("Operação Inválida.", execao.Message);
        }
        #endregion
    }
}