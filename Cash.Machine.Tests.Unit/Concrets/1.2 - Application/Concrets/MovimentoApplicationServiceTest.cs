using AutoMapper;
using Cash.Machine.Application.Concrets;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Application.Concrets
{
    public class MovimentoApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IMovementService> mockServiceMovimento;
        private readonly MovementApplicationService applicationServiceMovimento;
        private readonly MovimentoTestsFixture movimentoTestsFixture;
        private readonly CaixaEletronicoTestsFixture caixaEletronicoTestsFixture;
        #endregion

        #region Construtor
        public MovimentoApplicationServiceTest()
        {
            caixaEletronicoTestsFixture = new CaixaEletronicoTestsFixture();
            movimentoTestsFixture = new MovimentoTestsFixture();

            mockMapper = new Mock<IMapper>();
            mockServiceMovimento = new Mock<IMovementService>();
            
            applicationServiceMovimento = new MovementApplicationService(mockServiceMovimento.Object, mockMapper.Object);

        }
        #endregion

        #region Saque
        [Fact(DisplayName = "Efetuar Saque com Sucesso")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveExecutarSaqueSucesso()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Withdraw(saque.AccountId, saque.OperationId, saque.OperationAmount);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Conta Inválida")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueContaInvalida()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Withdraw(saque.AccountId, saque.OperationId, saque.OperationAmount));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Operação Inválida")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueOperacaoInvalida()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Withdraw(saque.AccountId, saque.OperationId, saque.OperationAmount));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                   It.IsAny<int>(),
                                                                                   It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Saldo Insuficiente")]
        [Trait("Saque", "Application Service Movimento")]
        public void DeveFalharSaqueSaldoInsuficiente()
        {
            //Arrange
            var saque = caixaEletronicoTestsFixture.GerarSaque();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
                                                                                  It.IsAny<int>(),
                                                                                  It.IsAny<decimal>()))
                                                                                  .Throws(new Exception("Saldo Insuficiente."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Withdraw(saque.AccountId, saque.OperationId, saque.OperationAmount));

            //Assert
            Assert.Equal("Saldo Insuficiente.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Withdraw(It.IsAny<int>(),
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

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Deposit(deposito.AccountId, deposito.OperationId, deposito.OperationAmount);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
                                                                                    It.IsAny<int>(),
                                                                                    It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Conta Inválida")]
        [Trait("Depósito", "Application Service Movimento")]
        public void DeveFalharDepositoContaInvalida()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()))
                                                                                    .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Deposit(deposito.AccountId, deposito.OperationId, deposito.OperationAmount));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Operação Inválida")]
        [Trait("Depósito", "Application Service Movimento")]
        public void DeveFalharDepositoOperacaoInvalida()
        {
            //Arrange
            var deposito = caixaEletronicoTestsFixture.GerarDeposito();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
                                                                                     It.IsAny<int>(),
                                                                                     It.IsAny<decimal>()))
                                                                                    .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Deposit(deposito.AccountId, deposito.OperationId, deposito.OperationAmount));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Deposit(It.IsAny<int>(),
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

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()));
            //Act
            applicationServiceMovimento.Payment(pagamento.AccountId, pagamento.OperationId, pagamento.OperationAmount, pagamento.BarCode);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
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
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Payment(pagamento.AccountId, pagamento.OperationId, pagamento.OperationAmount, pagamento.BarCode));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
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
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Payment(pagamento.AccountId, pagamento.OperationId, pagamento.OperationAmount, pagamento.BarCode));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
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
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Saldo Insuficiente."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Payment(pagamento.AccountId, pagamento.OperationId, pagamento.OperationAmount, pagamento.BarCode));

            //Assert
            Assert.Equal("Saldo Insuficiente.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
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
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
                                                                                      It.IsAny<int>(),
                                                                                      It.IsAny<decimal>(),
                                                                                      It.IsAny<string>()))
                                                                                     .Throws(new Exception("Código de Barras Inválido."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Payment(pagamento.AccountId, pagamento.OperationId, pagamento.OperationAmount, pagamento.BarCode));

            //Assert
            Assert.Equal("Código de Barras Inválido.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Payment(It.IsAny<int>(),
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

            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()));
            //Act
            applicationServiceMovimento.Monetize(rentabilizacao.AccountId, rentabilizacao.OperationId, rentabilizacao.Tax);

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
                                                                                            It.IsAny<int>(),
                                                                                            It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Conta Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoContaInvalida()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()))
                                                                                           .Throws(new Exception("Conta Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Monetize(rentabilizacao.AccountId, rentabilizacao.OperationId, rentabilizacao.Tax));

            //Assert
            Assert.Equal("Conta Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
                                                                                            It.IsAny<int>(),
                                                                                            It.IsAny<decimal>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Operação Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoOperacaoInvalida()
        {
            //Arrange
            var rentabilizacao = caixaEletronicoTestsFixture.GerarRentablizacao();
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
                                                                                           It.IsAny<int>(),
                                                                                           It.IsAny<decimal>()))
                                                                                           .Throws(new Exception("Operação Inválida."));

            //Act
            var execao = Assert.Throws<Exception>(() => mockServiceMovimento.Object.Monetize(rentabilizacao.AccountId, rentabilizacao.OperationId, rentabilizacao.Tax));

            //Assert
            Assert.Equal("Operação Inválida.", execao.Message);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Monetize(It.IsAny<int>(),
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
            var listaMovimentosMockDTO = new List<MovementDTO> { movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault() };
            var listaMovimentosMock = new List<Movement> { movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault() };

            mockMapper.Setup(mapper => mapper.Map<List<MovementDTO>>(It.IsAny<List<Movement>>())).Returns(listaMovimentosMockDTO);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.List()).Returns(listaMovimentosMock);

            // Act
            var movimentos = applicationServiceMovimento.List();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<MovementDTO>>(listaMovimentosMock), Times.Once);
            mockServiceMovimento.Verify(serviceOperacao => serviceOperacao.List(), Times.Once);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<MovementDTO>(It.IsAny<Movement>())).Returns(movimentoMockDTO);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Get(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var result = applicationServiceMovimento.Get(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<MovementDTO>(movimentoMock), Times.Once);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Get(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<Movement>(It.IsAny<MovementDTO>())).Returns(movimentoMock);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Add(It.IsAny<Movement>()));

            // Act
            applicationServiceMovimento.Add(movimentoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Movement>(movimentoMockDTO), Times.Once);
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveAtualizarMovimentoSucesso()
        {
            // Arrange
            var movimentoMockDTO = movimentoTestsFixture.GerarMovimentosDTO(1, 1).FirstOrDefault();
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockMapper.Setup(mapper => mapper.Map<Movement>(It.IsAny<MovementDTO>())).Returns(movimentoMock);
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Update(It.IsAny<Movement>()));

            // Act
            applicationServiceMovimento.Update(movimentoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Movement>(movimentoMockDTO), Times.Once);
            mockServiceMovimento.Verify(repositoryMovimento => repositoryMovimento.Update(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Movimento com Sucesso")]
        [Trait("Movimento", "ApplicationService Movimento")]
        public void DeveExcluirMovimentoSucesso()
        {
            // Arrange
            mockServiceMovimento.Setup(serviceMovimento => serviceMovimento.Delete(It.IsAny<int>()));

            // Act
            applicationServiceMovimento.Delete(new int());

            //Assert
            mockServiceMovimento.Verify(serviceMovimento => serviceMovimento.Delete(It.IsAny<int>()), Times.Once);
        } 
        #endregion
    }
}