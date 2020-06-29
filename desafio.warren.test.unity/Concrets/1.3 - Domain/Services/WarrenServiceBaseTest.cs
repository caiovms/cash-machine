using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Domain.Services
{
    public class WarrenServiceBaseTest
    {
        #region Variáveis
        private readonly object objetoMock = new object();
        private readonly Mock<IWarrenRepositoryBase<object>> warrenRepositoryBaseMock;
        private readonly WarrenServiceBase<object> serviceBase;
        #endregion

        #region Construtor
        public WarrenServiceBaseTest()
        {
            warrenRepositoryBaseMock = new Mock<IWarrenRepositoryBase<object>>();
            serviceBase = new WarrenServiceBase<object>(warrenRepositoryBaseMock.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Objetos com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveListarObjetosSucesso()
        {
            // Arrange
            var listaObjetosMock = new List<object> { objetoMock };

            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Listar()).Returns(listaObjetosMock);

            // Act
            var listaObjetos = serviceBase.Listar();

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Listar(), Times.Once);
            Assert.Equal(listaObjetosMock, listaObjetos);
        }

        [Fact(DisplayName = "Obter Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveObterObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Obter(It.IsAny<int>())).Returns(objetoMock);

            // Act
            var objeto = serviceBase.Obter(new int());

            // Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Obter(It.IsAny<int>()), Times.Once);
            Assert.Equal(objetoMock, objeto);
        }

        [Fact(DisplayName = "Inserir Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveInserirObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Inserir(It.IsAny<object>()));

            // Act
            serviceBase.Inserir(objetoMock);

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Inserir(It.IsAny<object>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveAtualizarObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Atualizar(It.IsAny<object>()));

            // Act
            serviceBase.Atualizar(objetoMock);

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Atualizar(It.IsAny<object>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveExcluirObjetoSucesso()
        {
            //Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Excluir(It.IsAny<int>()));

            //Act
            serviceBase.Excluir(new int());


            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Excluir(It.IsAny<int>()), Times.Once);
        }
    }
}