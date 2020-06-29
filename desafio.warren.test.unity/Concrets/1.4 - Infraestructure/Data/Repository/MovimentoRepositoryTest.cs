using desafio.warren.data.Context;
using desafio.warren.domain.Entities;
using desafio.warren.repository;
using desafio.warren.test.unity.DataTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Concrets.Infraestructure.Data.Repository
{
    public class MovimentoRepositoryTest
    {
        #region Variáveis
        private readonly Mock<WarrenContext> warrenContext = new Mock<WarrenContext>();
        private readonly Mock<DbSet<Movimento>> dbSetMock = new Mock<DbSet<Movimento>>();
        private readonly MovimentoTestsFixture movimentoTestsFixture = new MovimentoTestsFixture();
        private readonly WarrenRepositoryBase<Movimento> repositoryBase;
        #endregion

        #region Construtor
        public MovimentoRepositoryTest()
        {
            repositoryBase = new WarrenRepositoryBase<Movimento>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Movimentos com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveListarMovimentosSucesso()
        {
            // Arrange
            var listaMovimentosMock = new List<Movimento>();
            listaMovimentosMock.AddRange(movimentoTestsFixture.GerarMovimentos(1, 10));

            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.Provider).Returns(listaMovimentosMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.Expression).Returns(listaMovimentosMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.ElementType).Returns(listaMovimentosMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<object>>().Setup(movimento => movimento.GetEnumerator()).Returns(listaMovimentosMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Movimento>()).Returns(dbSetMock.Object);

            // Act
            var listaMovimentos = repositoryBase.Listar();

            // Assert
            warrenContext.Verify(context => context.Set<Movimento>());
            Assert.Equal(listaMovimentosMock, listaMovimentos);
        }

        [Fact(DisplayName = "Obter Movimento com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveObterMovimentoSucesso()
        {
            //Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();

            warrenContext.Setup(context => context.Set<Movimento>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(movimentoMock);

            // Act
            var movimento = repositoryBase.Obter(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Movimento>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(movimentoMock, movimento);
        }

        [Fact(DisplayName = "Inserir Movimento com Sucesso")]
        [Trait("Movimento", "Repository Movimento")]
        public void DeveInserirMovimentoSucesso()
        {
            // Arrange
            var movimentoMock = movimentoTestsFixture.GerarMovimentos(1, 1).FirstOrDefault();
            warrenContext.Setup(context => context.Set<Movimento>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Movimento>()));

            // Act
            repositoryBase.Inserir(movimentoMock);

            //Assert
            warrenContext.Verify(context => context.Set<Movimento>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Movimento>(movimento => movimento == movimentoMock)));
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
            warrenContext.Setup(context => context.Set<Movimento>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Movimento>()));

            // Act
            repositoryBase.Excluir(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Movimento>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Movimento>()));
        }
    }
}