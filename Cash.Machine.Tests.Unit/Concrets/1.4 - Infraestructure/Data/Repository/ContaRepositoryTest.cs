using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Repository;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Infraestructure.Data.Repository
{
    public class ContaRepositoryTest
    {
        #region Variáveis
        private readonly BaseRepository<Account> repositoryBase;
        private readonly Mock<DataContext> warrenContext = new Mock<DataContext>();
        private readonly Mock<DbSet<Account>> dbSetMock = new Mock<DbSet<Account>>();
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        #endregion

        #region Construtor
        public ContaRepositoryTest()
        {
            repositoryBase = new BaseRepository<Account>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMock = contaTestsFixture.GerarContas(10);

            dbSetMock.As<IQueryable<Account>>().Setup(conta => conta.Provider).Returns(listaContasMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Account>>().Setup(conta => conta.Expression).Returns(listaContasMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Account>>().Setup(conta => conta.ElementType).Returns(listaContasMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Account>>().Setup(conta => conta.GetEnumerator()).Returns(listaContasMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Account>()).Returns(dbSetMock.Object);

            // Act
            var listaContas = repositoryBase.List();

            // Assert
            warrenContext.Verify(context => context.Set<Account>());
            Assert.Equal(listaContasMock, listaContas);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();

            warrenContext.Setup(context => context.Set<Account>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(contaMock);

            // Act
            var conta = repositoryBase.Get(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Account>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(contaMock, conta);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "Repository Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            var contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
            warrenContext.Setup(context => context.Set<Account>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Account>()));

            // Act
            repositoryBase.Add(contaMock);

            //Assert
            warrenContext.Verify(context => context.Set<Account>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Account>(conta => conta == contaMock)));
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
            warrenContext.Setup(context => context.Set<Account>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Account>()));

            // Act
            repositoryBase.Delete(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Account>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Account>()));
        }
    }
}