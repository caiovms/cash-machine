using desafio.warren.domain.core.Abstracts.Repositories;
using desafio.warren.domain.Entities;
using desafio.warren.services.Services;
using desafio.warren.test.unity.DataTest.Fixtures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Domain.Services
{
    public class ContaServiceTest
    {
        #region Variáveis
        private readonly ContaService serviceConta;
        private readonly Mock<IContaRepository> repositoryConta;
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        private readonly Conta contaMock;
        #endregion

        #region Construtor
        public ContaServiceTest()
        {
            repositoryConta = new Mock<IContaRepository>();
            serviceConta = new ContaService(repositoryConta.Object);
            contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMock = new List<Conta>();
            listaContasMock.AddRange(contaTestsFixture.GerarContas(10));

            repositoryConta.Setup(repositoryConta => repositoryConta.Listar()).Returns(listaContasMock);

            // Act
            var listaContas = serviceConta.Listar();

            // Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Listar(), Times.Once);
            Assert.Equal(listaContasMock, listaContas);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            repositoryConta.Setup(repositoryConta => repositoryConta.Obter(It.IsAny<int>())).Returns(contaMock);

            // Act
            var conta = serviceConta.Obter(new int());

            // Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Obter(It.IsAny<int>()), Times.Once);
            Assert.Equal(contaMock, conta);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "Service Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            repositoryConta.Setup(repositoryConta => repositoryConta.Inserir(It.IsAny<Conta>()));

            // Act
            serviceConta.Inserir(contaMock);

            //Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Inserir(It.IsAny<Conta>()), Times.Once);
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
            repositoryConta.Setup(repositoryConta => repositoryConta.Excluir(It.IsAny<int>()));

            // Act
            serviceConta.Excluir(new int());

            //Assert
            repositoryConta.Verify(repositoryConta => repositoryConta.Excluir(It.IsAny<int>()), Times.Once);
        }
    }
}