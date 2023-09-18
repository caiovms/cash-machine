using Cash.Machine.Data.Context;
using Cash.Machine.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Infraestructure.Data.Repository
{
    public class WarrenRepositoryBaseTest
    {
        #region Variáveis
        private readonly Mock<DataContext> warrenContext = new Mock<DataContext>();
        private readonly Mock<DbSet<object>> dbSetMock = new Mock<DbSet<object>>();
        private readonly BaseRepository<object> repositoryBase;
        private readonly object entidadeMock;
        #endregion

        #region Construtor
        public WarrenRepositoryBaseTest()
        {
            entidadeMock = new object();
            repositoryBase = new BaseRepository<object>(warrenContext.Object);
        } 
        #endregion

        [Fact(DisplayName = "Listar Entidades com Sucesso")]
        [Trait("Entidade", "Repository Base")]
        public void DeveListarEntidadesSucesso()
        {
            // Arrange
            var listaEntidadesMock = new List<object> { entidadeMock };

            dbSetMock.As<IQueryable<object>>().Setup(objeto => objeto.Provider).Returns(listaEntidadesMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<object>>().Setup(objeto => objeto.Expression).Returns(listaEntidadesMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<object>>().Setup(objeto => objeto.ElementType).Returns(listaEntidadesMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<object>>().Setup(objeto => objeto.GetEnumerator()).Returns(listaEntidadesMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<object>()).Returns(dbSetMock.Object);

            // Act
            var listaObjetos = repositoryBase.List();

            // Assert
            Assert.Equal(listaEntidadesMock, listaObjetos);
        }

        [Fact(DisplayName = "Obter Entidade com Sucesso")]
        [Trait("Entidade", "Repository Base")]
        public void DeveObterEntidadeSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<object>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(entidadeMock);

            // Act
            repositoryBase.Get(new int());

            // Assert
            warrenContext.Verify(context => context.Set<object>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
        }

        [Fact(DisplayName = "Inserir Entidade com Sucesso")]
        [Trait("Entidade", "Repository Base")]
        public void DeveInserirEntidadeSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<object>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<object>()));

            // Act
            repositoryBase.Add(entidadeMock);

            //Assert
            warrenContext.Verify(context => context.Set<object>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<object>(objeto => objeto == entidadeMock)));
        }

        [Fact(DisplayName = "Atualizar Entidade com Sucesso")]
        [Trait("Entidade", "Repository Base")]
        public void DeveAtualizarEntidadeSucesso()
        {

        }

        [Fact(DisplayName = "Excluir Entidade com Sucesso")]
        [Trait("Entidade", "Repository Base")]
        public void DeveExcluirEntidadeSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<object>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(entidadeMock);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<object>()));

            // Act
            repositoryBase.Delete(new int());

            // Assert
            warrenContext.Verify(context => context.Set<object>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.Is<object>(objeto => objeto == entidadeMock)));
        }
    }
}