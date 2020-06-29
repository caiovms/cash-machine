using AutoMapper;
using desafio.warren.application.Abstracts;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System.Collections.Generic;

namespace desafio.warren.application.Concrets
{
    public class ContaApplicationService : IContaApplicationService
    {
        #region Variáveis
        private readonly IContaService serviceConta;
        private readonly IMapper mapper;
        #endregion

        #region Construtor
        public ContaApplicationService(IContaService serviceConta, IMapper mapper)
        {
            this.serviceConta = serviceConta;
            this.mapper = mapper;
        }
        #endregion

        public IEnumerable<ContaDTO> Listar()
        {
            var contas = serviceConta.Listar();

            return mapper.Map<List<ContaDTO>>(contas);
        }

        public ContaDTO Obter(int id)
        {
            var conta = serviceConta.Obter(id);

            return mapper.Map<ContaDTO>(conta);
        }

        public void Inserir(ContaDTO contaDTO)
        {
            var conta = mapper.Map<Conta>(contaDTO);

            serviceConta.Inserir(conta);
        }

        public void Atualizar(ContaDTO contaDTO)
        {
            var conta = mapper.Map<Conta>(contaDTO);

            serviceConta.Atualizar(conta);
        }

        public void Excluir(int id)
        {
            serviceConta.Excluir(id);
        } 
    }
}