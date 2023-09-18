using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Services.Services;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Domain.Services
{
    public class MovimentoServiceTest
    {
        #region Vari�veis
        private readonly ContaTestsFixture contaTestsFixture;
        private readonly MovimentoTestsFixture movimentoTestsFixture;
        private readonly OperacaoTestFixture operacaoTestFixture;
        private readonly Mock<IAccountRepository> mockRepositoryConta;
        private readonly Mock<IMovementRepository> mockRepositoryMovimento;
        private readonly Mock<IOperationRepository> mockRepositoryOperacao;
        private readonly MovementService serviceMovimento;
        private readonly decimal taxaRendimento = 0.01M;
        #endregion

        #region Construtor
        public MovimentoServiceTest()
        {
            contaTestsFixture = new ContaTestsFixture();
            movimentoTestsFixture = new MovimentoTestsFixture();
            operacaoTestFixture = new OperacaoTestFixture();

            mockRepositoryMovimento = new Mock<IMovementRepository>();
            mockRepositoryConta = new Mock<IAccountRepository>();
            mockRepositoryOperacao = new Mock<IOperationRepository>();

            serviceMovimento = new MovementService(mockRepositoryOperacao.Object, mockRepositoryConta.Object, mockRepositoryMovimento.Object);
        }
        #endregion

        #region Saque
        [Fact(DisplayName = "Efetuar Saque com Sucesso")]
        [Trait("Saque", "Service Movimento")]
        public void DeveExecutarSaqueSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.WITHDRAW);

            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()));
            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            serviceMovimento.Withdraw(contaMock.Id, operacaoMock.Id, 500);

            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Conta Inv�lida")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.WITHDRAW);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(contaMockInvalida);

            //Act
            try
            {
                serviceMovimento.Withdraw(0, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inv�lida.", mensagem);
        }

        [Fact(DisplayName = "Falhar Saque por Opera��o Inv�lida")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.WITHDRAW);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());

            //Act
            try
            {
                serviceMovimento.Withdraw(contaMock.Id, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Opera��o Inv�lida.", mensagem);
        }

        [Fact(DisplayName = "Falhar Saque por Saldo Insuficiente")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueSaldoInsuficiente()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.WITHDRAW);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            try
            {
                serviceMovimento.Withdraw(contaMock.Id, operacaoMock.Id, contaMock.Balance + 1);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Saldo Insuficiente.", mensagem);
        }
        #endregion

        #region Deposito
        [Fact(DisplayName = "Efetuar Dep�sito com Sucesso")]
        [Trait("Dep�sito", "Service Movimento")]
        public void DeveExecutarDepositoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.DEPOSIT);

            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()));
            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            serviceMovimento.Deposit(contaMock.Id, operacaoMock.Id, 500);


            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Dep�sito por Conta Inv�lida")]
        [Trait("Dep�sito", "Service Movimento")]
        public void DeveFalharDepositoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.DEPOSIT);

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaTestsFixture.GerarContaInvalida());
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Deposit(0, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inv�lida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Dep�sito por Opera��o Inv�lida")]
        [Trait("Dep�sito", "Service Movimento")]
        public void DeveFalharDepositoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.DEPOSIT);

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Deposit(contaMock.Id, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Opera��o Inv�lida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Never);
        }
        #endregion

        #region Pagamento
        [Fact(DisplayName = "Efetuar Pagamento com Sucesso")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveExecutarPagamentoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.PAYMENT);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()));

            //Act
            serviceMovimento.Payment(contaMock.Id, 500, operacaoMock.Id, movimentoMock.BarCode);

            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Conta Inv�lida")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.PAYMENT);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMockInvalida);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Payment(0, 500, operacaoMock.Id, movimentoMock.BarCode);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inv�lida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por Opera��o Inv�lida")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.PAYMENT);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Payment(contaMock.Id, operacaoMock.Id, 500, movimentoMock.BarCode);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Opera��o Inv�lida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por Saldo Insuficiente")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoSaldoInsuficiente()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.PAYMENT);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Payment(contaMock.Id, operacaoMock.Id, contaMock.Balance + 1, movimentoMock.BarCode);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Saldo Insuficiente.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por C�digo de Barras Inv�lido")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoCodigoBarrasInvalido()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.PAYMENT);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Payment(contaMock.Id, operacaoMock.Id, 500, null);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("C�digo de Barras Inv�lido.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()), Times.Never);
        }
        #endregion

        #region Rentabilizacao
        [Fact(DisplayName = "Efetuar Rentabiliza��o com Sucesso")]
        [Trait("Rentabiliza��o", "Service Movimento")]
        public void DeveExecutarRentabilizacaoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.MONETIZE);

            mockRepositoryConta.Setup(contaRepository => contaRepository.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(operacapRepository => operacapRepository.Get(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()));

            //Act
            serviceMovimento.Monetize(contaMock.Id, operacaoMock.Id, taxaRendimento);

            //Assert
            mockRepositoryConta.Verify(contaRepository => contaRepository.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryConta.Verify(operacapRepository => operacapRepository.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabiliza��o por Conta Inv�lida")]
        [Trait("Rentabiliza��o", "Service Movimento")]
        public void DeveFalharRentabilizacaoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.MONETIZE);
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();

            mockRepositoryConta.Setup(contaRepository => contaRepository.Get(It.IsAny<int>())).Returns(contaMockInvalida);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Monetize(0, operacaoMock.Id, taxaRendimento);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inv�lida.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Rentabiliza��o por Opera��o Inv�lida")]
        [Trait("Rentabiliza��o", "Service Movimento")]
        public void DeveFalharRentabilizacaoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)OperationType.MONETIZE);

            mockRepositoryConta.Setup(m => m.Get(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(m => m.Get(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            //Act
            try
            {
                serviceMovimento.Monetize(contaMock.Id, operacaoMock.Id, taxaRendimento);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Opera��o Inv�lida.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Add(It.IsAny<Movement>()), Times.Never);
        }
        #endregion

        #region Movimento
        [Fact(DisplayName = "Listar Movimentos com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveListarMovimentosSucesso()
        {
            // Arrange
            var listaMovimentosMock = new List<Movement>();
            listaMovimentosMock.AddRange(movimentoTestsFixture.GerarMovimentos(1, 10));

            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.List()).Returns(listaMovimentosMock);

            // Act
            var listaMovimentos = serviceMovimento.List();

            // Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.List(), Times.Once);
            Assert.Equal(listaMovimentosMock, listaMovimentos);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Get(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var movimento = serviceMovimento.Get(new int());

            // Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Get(It.IsAny<int>()), Times.Once);
            Assert.Equal(movimentoMock, movimento);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()));

            // Act
            serviceMovimento.Add(movimentoMock);

            //Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Add(It.IsAny<Movement>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveAtualizarMovimentoSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveExcluirMovimentoSucesso()
        {
            // Arrange
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Delete(It.IsAny<int>()));

            // Act
            serviceMovimento.Delete(new int());

            //Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Delete(It.IsAny<int>()), Times.Once);
        } 
        #endregion
    }
}