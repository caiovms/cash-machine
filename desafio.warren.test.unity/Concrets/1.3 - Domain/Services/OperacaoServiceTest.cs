using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;
using desafio.warren.services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Domain.Services
{
    public class OperacaoServiceTest
    {
        #region Variáveis
        private readonly OperacaoService serviceOperacao;
        private readonly Mock<IOperacaoRepository> repositoryOperacao;
        private readonly Operacao operacaoMock;
        #endregion

        #region Construtor
        public OperacaoServiceTest()
        {
            repositoryOperacao = new Mock<IOperacaoRepository>();
            serviceOperacao = new OperacaoService(repositoryOperacao.Object);
            operacaoMock = new Operacao { Id = 1, Descricao = "TESTE" };
        }
        #endregion

        [Fact(DisplayName = "Listar Operacoes com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacoesMock = new List<Operacao>() { operacaoMock };

            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Listar()).Returns(listaOperacoesMock);

            // Act
            var operacoes = serviceOperacao.Listar();

            // Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Listar(), Times.Once);
            Assert.Equal(listaOperacoesMock, operacoes);
        }

        [Fact(DisplayName = "Obter Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var operacao = serviceOperacao.Obter(new int());

            // Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Obter(It.IsAny<int>()), Times.Once);
            Assert.Equal(operacaoMock, operacao);
        }

        [Fact(DisplayName = "Inserir Operacao com Sucesso")]
        [Trait("Operacao", "Service Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Inserir(It.IsAny<Operacao>()));

            // Act
            serviceOperacao.Inserir(operacaoMock);

            //Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Inserir(It.IsAny<Operacao>()), Times.Once);
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
            repositoryOperacao.Setup(repositoryOperacao => repositoryOperacao.Excluir(It.IsAny<int>()));

            // Act
            serviceOperacao.Excluir(new int());

            //Assert
            repositoryOperacao.Verify(repositoryOperacao => repositoryOperacao.Excluir(It.IsAny<int>()), Times.Once);
        }
    }
}