using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Domain.Services
{
    public class OperacaoServiceTest
    {
        #region Variáveis
        private readonly OperationService serviceOperacao;
        private readonly Mock<IOperationRepository> repositoryOperacao;
        private readonly Operation operacaoMock;
        #endregion

        #region Construtor
        public OperacaoServiceTest()
        {
            repositoryOperacao = new Mock<IOperationRepository>();
            serviceOperacao = new OperationService(repositoryOperacao.Object);
            operacaoMock = new Operation { Id = 1, Description = "TESTE" };
        }
        #endregion

        [Fact(DisplayName = "Listar Operacoes com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacoesMock = new List<Operation>() { operacaoMock };

            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.List()).Returns(listaOperacoesMock);

            // Act
            var operacoes = serviceOperacao.List();

            // Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.List(), Times.Once);
            Assert.Equal(listaOperacoesMock, operacoes);
        }

        [Fact(DisplayName = "Obter Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var operacao = serviceOperacao.Get(new int());

            // Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Get(It.IsAny<int>()), Times.Once);
            Assert.Equal(operacaoMock, operacao);
        }

        [Fact(DisplayName = "Inserir Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Add(It.IsAny<Operation>()));

            // Act
            serviceOperacao.Add(operacaoMock);

            //Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Add(It.IsAny<Operation>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveAtualizarOperacaoSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveExcluirOperacaoSucesso()
        {
            // Arrange
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Delete(It.IsAny<int>()));

            // Act
            serviceOperacao.Delete(new int());

            //Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}