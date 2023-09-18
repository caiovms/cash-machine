using Cash.Machine.Data.Context;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Infraestructure.Data.Repository
{
    public class OperacaoRepositoryTest
    {
        #region Variáveis
        private readonly BaseRepository<Operation> repositoryBase;
        private readonly Mock<DataContext> warrenContext = new Mock<DataContext>();
        private readonly Mock<DbSet<Operation>> dbSetMock = new Mock<DbSet<Operation>>();
        private readonly Operation operacaoMock = new Operation { Id = 1, Description = "SAQUE" };
        #endregion

        #region Construtor
        public OperacaoRepositoryTest()
        {
            repositoryBase = new BaseRepository<Operation>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Operacões com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacaosMock = new List<Operation> { operacaoMock };

            dbSetMock.As<IQueryable<Operation>>().Setup(operacao => operacao.Provider).Returns(listaOperacaosMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Operation>>().Setup(operacao => operacao.Expression).Returns(listaOperacaosMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Operation>>().Setup(operacao => operacao.ElementType).Returns(listaOperacaosMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Operation>>().Setup(operacao => operacao.GetEnumerator()).Returns(listaOperacaosMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Operation>()).Returns(dbSetMock.Object);

            // Act
            var listaOperacoes = repositoryBase.List();

            // Assert
            warrenContext.Verify(context => context.Set<Operation>());
            Assert.Equal(listaOperacaosMock, listaOperacoes.ToList());
        }

        [Fact(DisplayName = "Obter Operacao com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            warrenContext.Setup(context => context.Set<Operation>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var operacao = repositoryBase.Get(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Operation>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(operacaoMock, operacao);
        }

        [Fact(DisplayName = "Inserir Operacao com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<Operation>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Operation>()));

            // Act
            repositoryBase.Add(operacaoMock);

            //Assert
            warrenContext.Verify(context => context.Set<Operation>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Operation>(Operacao => Operacao == operacaoMock)));
        }

        [Fact(DisplayName = "Atualizar Operacao com Sucesso")]
        [Trait("Operacao", "Repository Base")]
        public void DeveAtualizarOperacaoSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Operacao com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveExcluirOperacaoSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<Operation>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Operation>()));

            // Act
            repositoryBase.Delete(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Operation>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Operation>()));
        }
    }
}