using AutoMapper;
using desafio.warren.application.Abstracts;
using desafio.warren.application.dto;
using desafio.warren.domain.core.Abstracts.Services;
using desafio.warren.domain.Entities;
using System.Collections.Generic;

namespace desafio.warren.application.Concrets
{
    public class MovimentoApplicationService : IMovimentoApplicationService
    {
        #region Variáveis
        private readonly IMapper mapper;
        private readonly IMovimentoService serviceMovimento;
        #endregion

        #region Construtor
        public MovimentoApplicationService(IMovimentoService serviceMovimento, IMapper mapper)
        {
            this.serviceMovimento = serviceMovimento;
            this.mapper = mapper;
        }
        #endregion

        public void Saque(int idConta, int idOperacao, decimal valor)
        {
            serviceMovimento.Saque(idConta, idOperacao, valor);
        }
        
        public void Deposito(int idConta, int idOperacao, decimal valor)
        {
            serviceMovimento.Deposito(idConta, idOperacao, valor);
        }
        
        public void Pagamento(int idConta, int idOperacao, decimal valor, string codigoBarras)
        {
            serviceMovimento.Pagamento(idConta, idOperacao, valor, codigoBarras);
        }
       
        public void Rentabilizacao(int idConta, int idOperacao, decimal taxa)
        {
            serviceMovimento.Rentabilizacao(idConta, idOperacao, taxa);
        }

        public IEnumerable<MovimentoDTO> Listar()
        {
            var movimentos = serviceMovimento.Listar();

            return mapper.Map<List<MovimentoDTO>>(movimentos);
        }

        public MovimentoDTO Obter(int id)
        {
            var movimento = serviceMovimento.Obter(id);

            return mapper.Map<MovimentoDTO>(movimento);
        }

        public void Inserir(MovimentoDTO movimentoDTO)
        {
            var movimento = mapper.Map<Movimento>(movimentoDTO);

            serviceMovimento.Inserir(movimento);
        }

        public void Atualizar(MovimentoDTO movimentoDTO)
        {
            var movimento = mapper.Map<Movimento>(movimentoDTO);

            serviceMovimento.Atualizar(movimento);
        }

        public void Excluir(int id)
        {
            serviceMovimento.Excluir(id);
        }
    }
}
