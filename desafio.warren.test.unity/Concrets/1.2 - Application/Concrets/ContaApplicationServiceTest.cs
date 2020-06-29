using AutoMapper;
using desafio.warren.application.Concrets;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using desafio.warren.test.unity.DataTest.Fixtures;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Application.Concrets
{
    public class ContaApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IContaService> mockServiceConta;
        private readonly ContaApplicationService applicationServiceConta;
        private readonly ContaTestsFixture contaTestsFixture = new ContaTestsFixture();
        private readonly ContaDTO contaMockDTO;
        private readonly Conta contaMock;
        #endregion

        #region Construtor
        public ContaApplicationServiceTest()
        {
            mockMapper = new Mock<IMapper>();
            mockServiceConta = new Mock<IContaService>();
            applicationServiceConta = new ContaApplicationService(mockServiceConta.Object, mockMapper.Object);
            contaMockDTO = contaTestsFixture.GerarContasDTO(1).FirstOrDefault();
            contaMock = contaTestsFixture.GerarContas(1).FirstOrDefault();
        }
        #endregion

        [Fact(DisplayName = "Listar Contas com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveListarContasSucesso()
        {
            // Arrange
            var listaContasMockDTO = new List<ContaDTO> { contaMockDTO };
            var listaContasMock = new List<Conta> { contaMock };
            
            mockMapper.Setup(mapper => mapper.Map<List<ContaDTO>>(It.IsAny<List<Conta>>())).Returns(listaContasMockDTO);
            mockServiceConta.Setup(serviceOperacao => serviceOperacao.Listar()).Returns(listaContasMock);

            // Act
            var contas = applicationServiceConta.Listar();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<ContaDTO>>(listaContasMock), Times.Once);
            mockServiceConta.Verify(serviceOperacao => serviceOperacao.Listar(), Times.Once);
        }

        [Fact(DisplayName = "Obter Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveObterContaSucesso()
        {
            //Arrange
            mockMapper.Setup(mapper => mapper.Map<ContaDTO>(It.IsAny<Conta>())).Returns(contaMockDTO);
            mockServiceConta.Setup(serviceConta => serviceConta.Obter(It.IsAny<int>())).Returns(contaMock);

            // Act
            var result = applicationServiceConta.Obter(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<ContaDTO>(contaMock), Times.Once);
            mockServiceConta.Verify(serviceConta => serviceConta.Obter(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveInserirContaSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Conta>(It.IsAny<ContaDTO>())).Returns(contaMock);
            mockServiceConta.Setup(serviceConta => serviceConta.Inserir(It.IsAny<Conta>()));

            // Act
            applicationServiceConta.Inserir(contaMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Conta>(contaMockDTO), Times.Once);
            mockServiceConta.Verify(serviceOperacao => serviceOperacao.Inserir(It.IsAny<Conta>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveAtualizarContaSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Conta>(It.IsAny<ContaDTO>())).Returns(contaMock);
            mockServiceConta.Setup(serviceConta => serviceConta.Atualizar(It.IsAny<Conta>()));

            // Act
            applicationServiceConta.Atualizar(contaMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Conta>(contaMockDTO), Times.Once);
            mockServiceConta.Verify(serviceConta => serviceConta.Atualizar(It.IsAny<Conta>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Conta com Sucesso")]
        [Trait("Conta", "ApplicationService Conta")]
        public void DeveExcluirContaSucesso()
        {
            // Arrange
            mockServiceConta.Setup(serviceConta => serviceConta.Excluir(It.IsAny<int>()));

            // Act
            applicationServiceConta.Excluir(new int());

            //Assert
            mockServiceConta.Verify(serviceConta => serviceConta.Excluir(It.IsAny<int>()), Times.Once);
        }
    }
}