using AutoMapper;
using Cash.Machine.Application.Concrets;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using Cash.Machine.Tests.Unit.DataTest.Fixtures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Application.Concrets
{
    public class ContaApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IAccountService> mockServiceConta;
        private readonly AccountApplicationService applicationServiceConta;
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        private readonly AccountDTO contaMockDTO;
        private readonly Account contaMock;
        #endregion

        #region Construtor
        public ContaApplicationServiceTest()
        {
            mockMapper = new Mock<IMapper>();
            mockServiceConta = new Mock<IAccountService>();
            applicationServiceConta = new AccountApplicationService(mockServiceConta.Object, mockMapper.Object);
            contaMockDTO = contaTestsFixture.GerarContasDTO(1).FirstOrDefault();
            contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMockDTO = new List<AccountDTO> { contaMockDTO };
            var listaContasMock = new List<Account> { contaMock };
            
            mockMapper.Setup(mapper => mapper.Map<List<AccountDTO>>(It.IsAny<List<Account>>())).Returns(listaContasMockDTO);
            mockServiceConta.Setup(serviceOperacao => serviceOperacao.List()).Returns(listaContasMock);

            // Act
            var contas = applicationServiceConta.List();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<AccountDTO>>(listaContasMock), Times.Once);
            mockServiceConta.Verify(serviceOperacao => serviceOperacao.List(), Times.Once);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            mockMapper.Setup(mapper => mapper.Map<AccountDTO>(It.IsAny<Account>())).Returns(contaMockDTO);
            mockServiceConta.Setup(serviceConta => serviceConta.Get(It.IsAny<int>())).Returns(contaMock);

            // Act
            var result = applicationServiceConta.Get(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<AccountDTO>(contaMock), Times.Once);
            mockServiceConta.Verify(serviceConta => serviceConta.Get(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Account>(It.IsAny<AccountDTO>())).Returns(contaMock);
            mockServiceConta.Setup(serviceConta => serviceConta.Add(It.IsAny<Account>()));

            // Act
            applicationServiceConta.Add(contaMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Account>(contaMockDTO), Times.Once);
            mockServiceConta.Verify(serviceOperacao => serviceOperacao.Add(It.IsAny<Account>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveAtualizarContaSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Account>(It.IsAny<AccountDTO>())).Returns(contaMock);
            mockServiceConta.Setup(serviceConta => serviceConta.Update(It.IsAny<Account>()));

            // Act
            applicationServiceConta.Update(contaMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Account>(contaMockDTO), Times.Once);
            mockServiceConta.Verify(serviceConta => serviceConta.Update(It.IsAny<Account>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveExcluirContaSucesso()
        {
            // Arrange
            mockServiceConta.Setup(serviceConta => serviceConta.Delete(It.IsAny<int>()));

            // Act
            applicationServiceConta.Delete(new int());

            //Assert
            mockServiceConta.Verify(serviceConta => serviceConta.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}