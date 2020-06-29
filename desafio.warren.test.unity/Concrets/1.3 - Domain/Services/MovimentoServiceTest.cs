using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;
using desafio.warren.services.Services;
using desafio.warren.test.unity.DataTest.Fixtures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Domain.Services
{
    public class MovimentoServiceTest
    {
        #region Variáveis
        private readonly ContaTestsFixture contaTestsFixture;
        private readonly MovimentoTestsFixture movimentoTestsFixture;
        private readonly OperacaoTestFixture operacaoTestFixture;
        private readonly Mock<IContaRepository> mockRepositoryConta;
        private readonly Mock<IMovimentoRepository> mockRepositoryMovimento;
        private readonly Mock<IOperacaoRepository> mockRepositoryOperacao;
        private readonly MovimentoService serviceMovimento;
        private readonly decimal taxaRendimento = 0.01M;
        #endregion

        #region Construtor
        public MovimentoServiceTest()
        {
            contaTestsFixture = new ContaTestsFixture();
            movimentoTestsFixture = new MovimentoTestsFixture();
            operacaoTestFixture = new OperacaoTestFixture();

            mockRepositoryMovimento = new Mock<IMovimentoRepository>();
            mockRepositoryConta = new Mock<IContaRepository>();
            mockRepositoryOperacao = new Mock<IOperacaoRepository>();

            serviceMovimento = new MovimentoService(mockRepositoryOperacao.Object, mockRepositoryConta.Object, mockRepositoryMovimento.Object);
        }
        #endregion

        #region Saque
        [Fact(DisplayName = "Efetuar Saque com Sucesso")]
        [Trait("Saque", "Service Movimento")]
        public void DeveExecutarSaqueSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.SAQUE);

            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()));
            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            serviceMovimento.Saque(contaMock.Id, operacaoMock.Id, 500);

            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Saque por Conta Inválida")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.SAQUE);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(contaMockInvalida);

            //Act
            try
            {
                serviceMovimento.Saque(0, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inválida.", mensagem);
        }

        [Fact(DisplayName = "Falhar Saque por Operação Inválida")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.SAQUE);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());

            //Act
            try
            {
                serviceMovimento.Saque(contaMock.Id, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Operação Inválida.", mensagem);
        }

        [Fact(DisplayName = "Falhar Saque por Saldo Insuficiente")]
        [Trait("Saque", "Service Movimento")]
        public void DeveFalharSaqueSaldoInsuficiente()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.SAQUE);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            try
            {
                serviceMovimento.Saque(contaMock.Id, operacaoMock.Id, contaMock.Saldo + 1);
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
        [Fact(DisplayName = "Efetuar Depósito com Sucesso")]
        [Trait("Depósito", "Service Movimento")]
        public void DeveExecutarDepositoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.DEPOSITO);

            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()));
            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(operacaoMock);

            //Act
            serviceMovimento.Deposito(contaMock.Id, operacaoMock.Id, 500);


            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Depósito por Conta Inválida")]
        [Trait("Depósito", "Service Movimento")]
        public void DeveFalharDepositoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.DEPOSITO);

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaTestsFixture.GerarContaInvalida());
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Deposito(0, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inválida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Depósito por Operação Inválida")]
        [Trait("Depósito", "Service Movimento")]
        public void DeveFalharDepositoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.DEPOSITO);

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Deposito(contaMock.Id, operacaoMock.Id, 500);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Operação Inválida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Never);
        }
        #endregion

        #region Pagamento
        [Fact(DisplayName = "Efetuar Pagamento com Sucesso")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveExecutarPagamentoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.PAGAMENTO);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()));

            //Act
            serviceMovimento.Pagamento(contaMock.Id, 500, operacaoMock.Id, movimentoMock.CodigoBarras);

            //Assert
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Pagamento por Conta Inválida")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.PAGAMENTO);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMockInvalida);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Pagamento(0, 500, operacaoMock.Id, movimentoMock.CodigoBarras);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inválida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por Operação Inválida")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.PAGAMENTO);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Pagamento(contaMock.Id, operacaoMock.Id, 500, movimentoMock.CodigoBarras);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Operação Inválida.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por Saldo Insuficiente")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoSaldoInsuficiente()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.PAGAMENTO);
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Pagamento(contaMock.Id, operacaoMock.Id, contaMock.Saldo + 1, movimentoMock.CodigoBarras);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Saldo Insuficiente.", mensagem);
            mockRepositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Pagamento por Código de Barras Inválido")]
        [Trait("Pagamento", "Service Movimento")]
        public void DeveFalharPagamentoCodigoBarrasInvalido()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.PAGAMENTO);

            mockRepositoryConta.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(movimentoRepository => movimentoRepository.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Pagamento(contaMock.Id, operacaoMock.Id, 500, null);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Código de Barras Inválido.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()), Times.Never);
        }
        #endregion

        #region Rentabilizacao
        [Fact(DisplayName = "Efetuar Rentabilização com Sucesso")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveExecutarRentabilizacaoSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.RENTABILIZACAO);

            mockRepositoryConta.Setup(contaRepository => contaRepository.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(operacapRepository => operacapRepository.Obter(It.IsAny<int>())).Returns(operacaoMock);
            mockRepositoryMovimento.Setup(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()));

            //Act
            serviceMovimento.Rentabilizacao(contaMock.Id, operacaoMock.Id, taxaRendimento);

            //Assert
            mockRepositoryConta.Verify(contaRepository => contaRepository.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryConta.Verify(operacapRepository => operacapRepository.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()), Times.Once);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Conta Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoContaInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.RENTABILIZACAO);
            var contaMockInvalida = contaTestsFixture.GerarContaInvalida();

            mockRepositoryConta.Setup(contaRepository => contaRepository.Obter(It.IsAny<int>())).Returns(contaMockInvalida);
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Rentabilizacao(0, operacaoMock.Id, taxaRendimento);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Conta Inválida.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Never);
        }

        [Fact(DisplayName = "Falhar Rentabilização por Operação Inválida")]
        [Trait("Rentabilização", "Service Movimento")]
        public void DeveFalharRentabilizacaoOperacaoInvalida()
        {
            //Arrange
            var mensagem = string.Empty;
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            var operacaoMock = operacaoTestFixture.GerarOperacaoPorId((byte)TipoOperacao.RENTABILIZACAO);

            mockRepositoryConta.Setup(m => m.Obter(It.IsAny<int>())).Returns(contaMock);
            mockRepositoryOperacao.Setup(m => m.Obter(It.IsAny<int>())).Returns(operacaoTestFixture.GerarOperacaoInvalida());
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            //Act
            try
            {
                serviceMovimento.Rentabilizacao(contaMock.Id, operacaoMock.Id, taxaRendimento);
            }
            catch (ApplicationException exception)
            {
                mensagem = exception.Message;
            }

            //Assert
            Assert.NotEmpty(mensagem);
            Assert.Equal("Operação Inválida.", mensagem);
            mockRepositoryConta.Verify(contaRepository => contaRepository.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            mockRepositoryMovimento.Verify(movimentoRepository => movimentoRepository.Inserir(It.IsAny<Movimento>()), Times.Never);
        }
        #endregion

        #region Movimento
        [Fact(DisplayName = "Listar Movimentos com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveListarMovimentosSucesso()
        {
            // Arrange
            var listaMovimentosMock = new List<Movimento>();
            listaMovimentosMock.AddRange(movimentoTestsFixture.GerarMovimentos(1, 10));

            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Listar()).Returns(listaMovimentosMock);

            // Act
            var listaMovimentos = serviceMovimento.Listar();

            // Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Listar(), Times.Once);
            Assert.Equal(listaMovimentosMock, listaMovimentos);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Obter(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var movimento = serviceMovimento.Obter(new int());

            // Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Obter(It.IsAny<int>()), Times.Once);
            Assert.Equal(movimentoMock, movimento);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "Service Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()));

            // Act
            serviceMovimento.Inserir(movimentoMock);

            //Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Inserir(It.IsAny<Movimento>()), Times.Once);
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
            mockRepositoryMovimento.Setup(repositoryMovimento => repositoryMovimento.Excluir(It.IsAny<int>()));

            // Act
            serviceMovimento.Excluir(new int());

            //Assert
            mockRepositoryMovimento.Verify(repositoryMovimento => repositoryMovimento.Excluir(It.IsAny<int>()), Times.Once);
        } 
        #endregion
    }
}