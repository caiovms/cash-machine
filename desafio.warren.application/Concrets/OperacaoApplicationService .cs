using AutoMapper;
using desafio.warren.application.Abstracts;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System.Collections.Generic;

namespace desafio.warren.application.Concrets
{
    public class OperacaoApplicationService : IOperacaoApplicationService
    {
        #region Variáveis
        private readonly IMapper mapper;
        private readonly IOperacaoService serviceOperacao;
        #endregion

        #region Construtor
        public OperacaoApplicationService(IOperacaoService serviceOperacao, IMapper mapper)
        {
            this.mapper = mapper;
            this.serviceOperacao = serviceOperacao;
        }
        #endregion

        public IEnumerable<OperacaoDTO> Listar()
        {
            var operacoes = serviceOperacao.Listar();

            return mapper.Map<List<OperacaoDTO>>(operacoes);
        }

        public OperacaoDTO Obter(int id)
        {
            var operacao = serviceOperacao.Obter(id);

            return mapper.Map<OperacaoDTO>(operacao);
        }

        public void Inserir(OperacaoDTO operacaoDTO)
        {
            var operacao = mapper.Map<Operacao>(operacaoDTO);

            serviceOperacao.Inserir(operacao);
        }

        public void Atualizar(OperacaoDTO operacaoDTO)
        {
            var operacao = mapper.Map<Operacao>(operacaoDTO);

            serviceOperacao.Atualizar(operacao);
        }

        public void Excluir(int id)
        {
            serviceOperacao.Excluir(id);
        } 
    }
}