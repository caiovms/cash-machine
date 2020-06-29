using desafio.warren.data.Context;
using desafio.warren.domain.Entities;
using desafio.warren.repository;
using desafio.warren.test.unity.DataTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Infraestructure.Data.Repository
{
    public class ContaRepositoryTest
    {
        #region Variáveis
        private readonly WarrenRepositoryBase<Conta> repositoryBase;
        private readonly Mock<WarrenContext> warrenContext = new Mock<WarrenContext>();
        private readonly Mock<DbSet<Conta>> dbSetMock = new Mock<DbSet<Conta>>();
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        #endregion

        #region Construtor
        public ContaRepositoryTest()
        {
            repositoryBase = new WarrenRepositoryBase<Conta>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMock = contaTestsFixture.GerarContas(10);

            dbSetMock.As<IQueryable<Conta>>().Setup(conta => conta.Provider).Returns(listaContasMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Conta>>().Setup(conta => conta.Expression).Returns(listaContasMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Conta>>().Setup(conta => conta.ElementType).Returns(listaContasMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Conta>>().Setup(conta => conta.GetEnumerator()).Returns(listaContasMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Conta>()).Returns(dbSetMock.Object);

            // Act
            var listaContas = repositoryBase.Listar();

            // Assert
            warrenContext.Verify(context => context.Set<Conta>());
            Assert.Equal(listaContasMock, listaContas);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();

            warrenContext.Setup(context => context.Set<Conta>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(contaMock);

            // Act
            var conta = repositoryBase.Obter(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Conta>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(contaMock, conta);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            warrenContext.Setup(context => context.Set<Conta>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Conta>()));

            // Act
            repositoryBase.Inserir(contaMock);

            //Assert
            warrenContext.Verify(context => context.Set<Conta>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Conta>(conta => conta == contaMock)));
        }

        [Fact(DisplayName = "Atualizar Conta com Sucesso")]
        [Trait("Conta", "Repository Base")]
        public void DeveAtualizarContaSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Conta com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveExcluirContaSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<Conta>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Conta>()));

            // Act
            repositoryBase.Excluir(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Conta>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Conta>()));
        }
    }
}