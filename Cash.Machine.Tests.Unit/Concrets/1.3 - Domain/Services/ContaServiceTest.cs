using Cash.Machine.Domain.Core.Abstracts.Repositories;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Services.Services;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Domain.Services
{
    public class ContaServiceTest
    {
        #region Variáveis
        private readonly AccountService serviceConta;
        private readonly Mock<IAccountRepository> repositoryConta;
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        private readonly Account contaMock;
        #endregion

        #region Construtor
        public ContaServiceTest()
        {
            repositoryConta = new Mock<IAccountRepository>();
            serviceConta = new AccountService(repositoryConta.Object);
            contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMock = new List<Account>();
            listaContasMock.AddRange(contaTestsFixture.GerarContas(10));

            repositoryConta.Setup(repositoryConta => repositoryConta.List()).Returns(listaContasMock);

            // Act
            var listaContas = serviceConta.List();

            // Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.List(), Times.Once);
            Assert.Equal(listaContasMock, listaContas);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            repositoryConta.Setup(repositoryConta => repositoryConta.Get(It.IsAny<int>())).Returns(contaMock);

            // Act
            var conta = serviceConta.Get(new int());

            // Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Get(It.IsAny<int>()), Times.Once);
            Assert.Equal(contaMock, conta);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            repositoryConta.Setup(repositoryConta => repositoryConta.Add(It.IsAny<Account>()));

            // Act
            serviceConta.Add(contaMock);

            //Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Add(It.IsAny<Account>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveAtualizarContaSucesso()
        {
        }

        [Fact(DisplayName = "Excluir Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveExcluirContaSucesso()
        {
            // Arrange
            repositoryConta.Setup(repositoryConta => repositoryConta.Delete(It.IsAny<int>()));

            // Act
            serviceConta.Delete(new int());

            //Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}