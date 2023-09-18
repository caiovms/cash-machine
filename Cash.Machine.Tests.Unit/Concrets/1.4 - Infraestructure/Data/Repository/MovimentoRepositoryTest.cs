using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Repository;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Concrets.Infraestructure.Data.Repository
{
    public class MovimentoRepositoryTest
    {
        #region Variáveis
        private readonly Mock<DataContext> warrenContext = new Mock<DataContext>();
        private readonly Mock<DbSet<Movement>> dbSetMock = new Mock<DbSet<Movement>>();
        private readonly MovimentoTestsFixture movimentoTestsFixture = new MovimentoTestsFixture();
        private readonly BaseRepository<Movement> repositoryBase;
        #endregion

        #region Construtor
        public MovimentoRepositoryTest()
        {
            repositoryBase = new BaseRepository<Movement>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Movimentos com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveListarMovimentosSucesso()
        {
            // Arrange
            var listaMovimentosMock = new List<Movement>();
            listaMovimentosMock.AddRange(movimentoTestsFixture.GerarMovimentos(1, 10));

            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.Provider).Returns(listaMovimentosMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.Expression).Returns(listaMovimentosMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.ElementType).Returns(listaMovimentosMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.GetEnumerator()).Returns(listaMovimentosMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Movement>()).Returns(dbSetMock.Object);

            // Act
            var listaMovimentos = repositoryBase.List();

            // Assert
            warrenContext.Verify(context => context.Set<Movement>());
            Assert.Equal(listaMovimentosMock, listaMovimentos);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            warrenContext.Setup(context => context.Set<Movement>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var movimento = repositoryBase.Get(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Movement>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(movimentoMock, movimento);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();
            warrenContext.Setup(context => context.Set<Movement>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Movement>()));

            // Act
            repositoryBase.Add(movimentoMock);

            //Assert
            warrenContext.Verify(context => context.Set<Movement>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Movement>(movimento => movimento == movimentoMock)));
        }

        [Fact(DisplayName = "Atualizar Movimento com Sucesso")]
        [Trait("Movimento", "Repository Base")]
        public void DeveAtualizarMovimentoSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Movimento com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveExcluirMovimentoSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<Movement>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Movement>()));

            // Act
            repositoryBase.Delete(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Movement>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Movement>()));
        }
    }
}