using AutoMapper;
using desafio.warren.application.Concrets;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using desafio.warren.test.unity.DataTest.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Application.Concrets
{
    public class MovimentoApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IMovimentoService> mockServiceMovimento;
        private readonly MovimentoApplicationService applicationServiceMovimento;
        private readonly MovimentoTestsFixture movimentoTestsFixture;
        private readonly CaixaEletronicoTestsFixture caixaEletronicoTestsFixture;
        #endregion

        #region Construtor
        public MovimentoApplicationServiceTest()
        {
            caixaEletronicoTestsFixture = new CaixaEletronicoTestsFixture();
            movimentoTestsFixture = new MovimentoTestsFixture();

            mockMapper = new Mock<IMapper>();
            mockServiceMovimento = new Mock<IMovimentoService>();
            
            applicationServiceMovimento = new MovimentoApplicationService(mockServiceMovimento.Object, mockMapper.Object);

        }
        #endregion

        #region Saque
        [Fact(DisplayName = "Efetuar Saque com Sucesso")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveExecutarSaqueSucesso()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Conta Inválida")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueContaInvalida()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Operação Inválida")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueOperacaoInvalida()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Saldo Insuficiente")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueSaldoInsuficiente()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Saldo Insuficiente."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Saque(saque.IdConta, saque.IdOperacao, saque.ValorOperacao));

            //Assert
            Assert.Equal("Saldo Insuficiente.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Saque(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }
        #endregion

        #region Deposito
        [Fact(DisplayName = "Efetuar Depósito com Sucesso")]
        [Trait("Depósito", "Application Service Movimento")]
        public void DeveExecutarDepositoSucesso()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Deposito(deposito.IdConta, deposito.IdOperacao, deposito.ValorOperacao);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                    It.IsAny<int>(),
                                                                                    It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Conta Inválida")]
        [Trait("Depósito", "Application Service Movimento")]
        public void DeveFalharDepositoContaInvalida()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()))
                                                                                    .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Deposito(deposito.IdConta, deposito.IdOperacao, deposito.ValorOperacao));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Operação Inválida")]
        [Trait("Depósito", "Application Service Movimento")]
        public void DeveFalharDepositoOperacaoInvalida()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()))
                                                                                    .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Deposito(deposito.IdConta, deposito.IdOperacao, deposito.ValorOperacao));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposito(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>()), Times.Once);
        }
        #endregion

        #region Pagamento
        [Fact(DisplayName = "Efetuar Pagamento com Sucesso")]
        [Trait("Pagamento", "Application Service Movimento")]
        public void DeveExecutarPagamentoSucesso()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()));
            //Act
            applicationServiceMovimento.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                       It.IsAny<int>(),
                                                                                       It.IsAny<decimal>(),
                                                                                       It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Conta Inválida")]
        [Trait("Pagamento", "Application Service Movimento")]
        public void DeveFalharPagamentoContaInvalida()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Operação Inválida")]
        [Trait("Pagamento", "Application Service Movimento")]
        public void DeveFalharPagamentoOperacaoInvalida()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Saldo Insuficiente")]
        [Trait("Pagamento", "Application Service Movimento")]
        public void DeveFalharPagamentoSaldoInsuficiente()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Saldo Insuficiente."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras));

            //Assert
            Assert.Equal("Saldo Insuficiente.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Código de Barras Inválido")]
        [Trait("Pagamento", "Application Service Movimento")]
        public void DeveFalharPagamentoCodigoBarrasInvalido()
        {
            //Arrange
            var pagamento = caixaEletronicoTestsFixture.GerarPagamento();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Código de Barras Inválido."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Pagamento(pagamento.IdConta, pagamento.IdOperacao, pagamento.ValorOperacao, pagamento.CodigoDeBarras));

            //Assert
            Assert.Equal("Código de Barras Inválido.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Pagamento(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region Rentabilizacao
        [Fact(DisplayName = "Efetuar Rentabilização com Sucesso")]
        [Trait("Rentabilização", "Application Service Movimento")]
        public void DeveExecutarRentabilizacaoSucesso()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Rentabilizacao(rentabilizacao.IdConta, rentabilizacao.IdOperacao, rentabilizacao.TaxaRentabilidade);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                            It.IsAny<int>(),
                                                                                            It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Conta Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoContaInvalida()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()))
                                                                                           .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Rentabilizacao(rentabilizacao.IdConta, rentabilizacao.IdOperacao, rentabilizacao.TaxaRentabilidade));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                            It.IsAny<int>(),
                                                                                            It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Operação Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoOperacaoInvalida()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()))
                                                                                           .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Rentabilizacao(rentabilizacao.IdConta, rentabilizacao.IdOperacao, rentabilizacao.TaxaRentabilidade));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Rentabilizacao(It.IsAny<int>(),
                                                                                            It.IsAny<int>(),
                                                                                            It.IsAny<decimal>()), Times.Once);
        }
        #endregion

        #region Movimento
        [Fact(DisplayName = "Listar Movimentos com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveListarMovimentosSucesso()
        {
            // Arrange
            var listaMovimentosMockDTO = new List<MovimentoDTO> { movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault() };
            var listaMovimentosMock = new List<Movimento> { movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault() };

            mockMapper.Setup(mapper => mapper.Map<List<MovimentoDTO>>(It.IsAny<List<Movimento>>())).Returns(listaMovimentosMockDTO);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Listar()).Returns(listaMovimentosMock);

            // Act
            var movimentos = applicationServiceMovimento.Listar();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<MovimentoDTO>>(listaMovimentosMock), Times.Once);
            mockServiceMovimento.Verify(serviceOperacao => serviceOperacao.Listar(), Times.Once);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<MovimentoDTO>(It.IsAny<Movimento>())).Returns(movimentoMockDTO);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Obter(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var result = applicationServiceMovimento.Obter(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<MovimentoDTO>(movimentoMock), Times.Once);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Obter(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<Movimento>(It.IsAny<MovimentoDTO>())).Returns(movimentoMock);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Inserir(It.IsAny<Movimento>()));

            // Act
            applicationServiceMovimento.Inserir(movimentoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Movimento>(movimentoMockDTO), Times.Once);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Inserir(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveAtualizarMovimentoSucesso()
        {
            // Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<Movimento>(It.IsAny<MovimentoDTO>())).Returns(movimentoMock);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Atualizar(It.IsAny<Movimento>()));

            // Act
            applicationServiceMovimento.Atualizar(movimentoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Movimento>(movimentoMockDTO), Times.Once);
            mockServiceMovimento.Verify(repositoryMovimento => repositoryMovimento.Atualizar(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveExcluirMovimentoSucesso()
        {
            // Arrange
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Excluir(It.IsAny<int>()));

            // Act
            applicationServiceMovimento.Excluir(new int());

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Excluir(It.IsAny<int>()), Times.Once);
        } 
        #endregion
    }
}