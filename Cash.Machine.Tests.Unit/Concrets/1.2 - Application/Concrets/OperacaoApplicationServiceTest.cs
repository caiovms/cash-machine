using AutoMapper;
using Cash.Machine.Application.Concrets;
using Cash.Machine.Application.DTO;
using Cash.Machine.Domain.Core.Abstracts.Services;
using Cash.Machine.Domain.Entities;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Cash.Machine.Tests.Unit.Concrets.Application.Concrets
{
    public class OperacaoApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IOperationService> mockServiceOperacao;
        private readonly OperationApplicationService applicationServiceOperacao;
        private readonly OperationDTO operacaoMockDTO;
        private readonly Operation operacaoMock;
        #endregion

        #region Construtor
        public OperacaoApplicationServiceTest()
        {
            mockMapper = new Mock<IMapper>();
            mockServiceOperacao = new Mock<IOperationService>();
            applicationServiceOperacao = new OperationApplicationService(mockServiceOperacao.Object, mockMapper.Object);
            operacaoMockDTO = new OperationDTO { Id = 1, Description = "TESTE"};
            operacaoMock = new Operation { Id = 1, Description = "TESTE"};
        }
        #endregion

        [Fact(DisplayName = "Listar Operacões com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacoesMockDTO = new List<OperationDTO> { operacaoMockDTO };
            var listaOperacoesMock = new List<Operation> { operacaoMock };

            mockMapper.Setup(mapper => mapper.Map<List<OperationDTO>>(It.IsAny<List<Operation>>())).Returns(listaOperacoesMockDTO);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.List()).Returns(listaOperacoesMock);

            // Act
            var listaOperacoes = applicationServiceOperacao.List();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<OperationDTO>>(listaOperacoesMock), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.List(), Times.Once);
        }

        [Fact(DisplayName = "Obter Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            mockMapper.Setup(mapper => mapper.Map<OperationDTO>(It.IsAny<Operation>())).Returns(operacaoMockDTO);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Get(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var result = applicationServiceOperacao.Get(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<OperationDTO>(operacaoMock), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Get(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Operation>(It.IsAny<OperationDTO>())).Returns(operacaoMock);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Add(It.IsAny<Operation>()));

            // Act
            applicationServiceOperacao.Add(operacaoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Operation>(operacaoMockDTO), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Add(It.IsAny<Operation>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveAtualizarOperacaoSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Operation>(It.IsAny<OperationDTO>())).Returns(operacaoMock);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Update(It.IsAny<Operation>()));

            // Act
            applicationServiceOperacao.Update(operacaoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Operation>(operacaoMockDTO), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Update(It.IsAny<Operation>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveExcluirOperacaoSucesso()
        {
            // Arrange
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Delete(It.IsAny<int>()));

            // Act
            applicationServiceOperacao.Delete(new int());

            //Assert
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}