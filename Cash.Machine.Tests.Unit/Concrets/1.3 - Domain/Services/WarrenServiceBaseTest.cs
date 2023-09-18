using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Services.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Domain.Services
{
    public class WarrenServiceBaseTest
    {
        #region Variáveis
        private readonly object objetoMock = new object();
        private readonly Mock<IRepositoryBase<object>> warrenRepositoryBaseMock;
        private readonly ServiceBase<object> serviceBase;
        #endregion

        #region Construtor
        public WarrenServiceBaseTest()
        {
            warrenRepositoryBaseMock = new Mock<IRepositoryBase<object>>();
            serviceBase = new ServiceBase<object>(warrenRepositoryBaseMock.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Objetos com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveListarObjetosSucesso()
        {
            // Arrange
            var listaObjetosMock = new List<object> { objetoMock };

            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.List()).Returns(listaObjetosMock);

            // Act
            var listaObjetos = serviceBase.List();

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.List(), Times.Once);
            Assert.Equal(listaObjetosMock, listaObjetos);
        }

        [Fact(DisplayName = "Obter Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveObterObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Get(It.IsAny<int>())).Returns(objetoMock);

            // Act
            var objeto = serviceBase.Get(new int());

            // Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Get(It.IsAny<int>()), Times.Once);
            Assert.Equal(objetoMock, objeto);
        }

        [Fact(DisplayName = "Inserir Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveInserirObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Add(It.IsAny<object>()));

            // Act
            serviceBase.Add(objetoMock);

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Add(It.IsAny<object>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveAtualizarObjetoSucesso()
        {
            // Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Update(It.IsAny<object>()));

            // Act
            serviceBase.Update(objetoMock);

            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Update(It.IsAny<object>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Objeto com Sucesso")]
        [Trait("Objeto", "Service Base")]
        public void DeveExcluirObjetoSucesso()
        {
            //Arrange
            warrenRepositoryBaseMock.Setup(repositoryBase => repositoryBase.Delete(It.IsAny<int>()));

            //Act
            serviceBase.Delete(new int());


            //Assert
            warrenRepositoryBaseMock.Verify(repositoryBase => repositoryBase.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}