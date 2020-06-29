using AutoMapper;
using desafio.warren.application.Concrets;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace desafio.warren.test.unity.Concrets.Application.Concrets
{
    public class OperacaoApplicationServiceTest
    {
        #region Variáveis
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<IOperacaoService> mockServiceOperacao;
        private readonly OperacaoApplicationService applicationServiceOperacao;
        private readonly OperacaoDTO operacaoMockDTO;
        private readonly Operacao operacaoMock;
        #endregion

        #region Construtor
        public OperacaoApplicationServiceTest()
        {
            mockMapper = new Mock<IMapper>();
            mockServiceOperacao = new Mock<IOperacaoService>();
            applicationServiceOperacao = new OperacaoApplicationService(mockServiceOperacao.Object, mockMapper.Object);
            operacaoMockDTO = new OperacaoDTO { Id = 1, Descricao = "TESTE"};
            operacaoMock = new Operacao { Id = 1, Descricao = "TESTE"};
        }
        #endregion

        [Fact(DisplayName = "Listar Operacões com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveListarOperacoesSucesso()
        {
            // Arrange
            var listaOperacoesMockDTO = new List<OperacaoDTO> { operacaoMockDTO };
            var listaOperacoesMock = new List<Operacao> { operacaoMock };

            mockMapper.Setup(mapper => mapper.Map<List<OperacaoDTO>>(It.IsAny<List<Operacao>>())).Returns(listaOperacoesMockDTO);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Listar()).Returns(listaOperacoesMock);

            // Act
            var listaOperacoes = applicationServiceOperacao.Listar();

            // Assert
            mockMapper.Verify(mapper => mapper.Map<List<OperacaoDTO>>(listaOperacoesMock), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Listar(), Times.Once);
        }

        [Fact(DisplayName = "Obter Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveObterOperacaoSucesso()
        {
            //Arrange
            mockMapper.Setup(mapper => mapper.Map<OperacaoDTO>(It.IsAny<Operacao>())).Returns(operacaoMockDTO);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Obter(It.IsAny<int>())).Returns(operacaoMock);

            // Act
            var result = applicationServiceOperacao.Obter(new int());

            // Assert
            mockMapper.Verify(mapper => mapper.Map<OperacaoDTO>(operacaoMock), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Obter(It.IsAny<int>()), Times.Once);
        }

        [Fact(DisplayName = "Inserir Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveInserirOperacaoSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Operacao>(It.IsAny<OperacaoDTO>())).Returns(operacaoMock);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Inserir(It.IsAny<Operacao>()));

            // Act
            applicationServiceOperacao.Inserir(operacaoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Operacao>(operacaoMockDTO), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Inserir(It.IsAny<Operacao>()), Times.Once);
        }

        [Fact(DisplayName = "Atualizar Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveAtualizarOperacaoSucesso()
        {
            // Arrange
            mockMapper.Setup(mapper => mapper.Map<Operacao>(It.IsAny<OperacaoDTO>())).Returns(operacaoMock);
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Atualizar(It.IsAny<Operacao>()));

            // Act
            applicationServiceOperacao.Atualizar(operacaoMockDTO);

            //Assert
            mockMapper.Verify(mapper => mapper.Map<Operacao>(operacaoMockDTO), Times.Once);
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Atualizar(It.IsAny<Operacao>()), Times.Once);
        }

        [Fact(DisplayName = "Excluir Operacão com Sucesso")]
        [Trait("Operacao", "ApplicationService Operacao")]
        public void DeveExcluirOperacaoSucesso()
        {
            // Arrange
            mockServiceOperacao.Setup(serviceOperacao => serviceOperacao.Excluir(It.IsAny<int>()));

            // Act
            applicationServiceOperacao.Excluir(new int());

            //Assert
            mockServiceOperacao.Verify(serviceOperacao => serviceOperacao.Excluir(It.IsAny<int>()), Times.Once);
        }
    }
}