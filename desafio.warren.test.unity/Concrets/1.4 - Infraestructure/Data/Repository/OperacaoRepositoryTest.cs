using desafio.warren.data.Context;
using desafio.warren.domain.Entities;
using desafio.warren.repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Infraestructure.Data.Repository
{
    public class OperacaoRepositoryTest
    {
        #region Variáveis
        private readonly WarrenRepositoryBase<Operacao> repositoryBase;
        private readonly Mock<WarrenContext> warrenContext = new Mock<WarrenContext>();
        private readonly Mock<DbSet<Operacao>> dbSetMock = new Mock<DbSet<Operacao>>();
        private readonly Operacao operacaoMock = new Operacao { Id = 1, Descricao = "SAQUE" };
        #endregion

        #region Construtor
        public OperacaoRepositoryTest()
        {
            repositoryBase = new WarrenRepositoryBase<Operacao>(warrenContext.Object);
        }
        #endregion

        [Fact(DisplayName = "Listar Operacões com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacaosMock = new List<Operacao> { operacaoMock };

            dbSetMock.As<IQueryable<Operacao>>().Setup(operacao => operacao.Provider).Returns(listaOperacaosMock.AsQueryable().Provider);
            dbSetMock.As<IQueryable<Operacao>>().Setup(operacao => operacao.Expression).Returns(listaOperacaosMock.AsQueryable().Expression);
            dbSetMock.As<IQueryable<Operacao>>().Setup(operacao => operacao.ElementType).Returns(listaOperacaosMock.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<Operacao>>().Setup(operacao => operacao.GetEnumerator()).Returns(listaOperacaosMock.AsQueryable().GetEnumerator());

            warrenContext.Setup(context => context.Set<Operacao>()).Returns(dbSetMock.Object);

            // Act
            var listaOperacoes = repositoryBase.Listar();

            // Assert
            warrenContext.Verify(context => context.Set<Operacao>());
            Assert.Equal(listaOperacaosMock, listaOperacoes.ToList());
        }

        [Fact(DisplayName = "Obter Operacao com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            warrenContext.Setup(context => context.Set<Operacao>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Find(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var operacao = repositoryBase.Obter(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Operacao>());
            dbSetMock.Verify(dbSet => dbSet.Find(It.IsAny<int>()));
            Assert.Equal(operacaoMock, operacao);
        }

        [Fact(DisplayName = "Inserir Operacao com Sucesso")]
        [Trait("Operacao", "Repository Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            warrenContext.Setup(context => context.Set<Operacao>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Add(It.IsAny<Operacao>()));

            // Act
            repositoryBase.Inserir(operacaoMock);

            //Assert
            warrenContext.Verify(context => context.Set<Operacao>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Add(It.Is<Operacao>(Operacao => Operacao == operacaoMock)));
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
            warrenContext.Setup(context => context.Set<Operacao>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(dbSet => dbSet.Remove(It.IsAny<Operacao>()));

            // Act
            repositoryBase.Excluir(new int());

            // Assert
            warrenContext.Verify(context => context.Set<Operacao>());
            warrenContext.Verify(context => context.SaveChanges(), Times.Once);
            dbSetMock.Verify(dbSet => dbSet.Remove(It.IsAny<Operacao>()));
        }
    }
}