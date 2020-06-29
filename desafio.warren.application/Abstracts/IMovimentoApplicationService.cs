using desafio.warren.application.dto;
using System.Collections.Generic;

namespace desafio.warren.application.Abstracts
{
    public interface IMovimentoApplicationService
    {
        void Saque(int idConta, int idOperacao, decimal valor);
        
        void Deposito(int idConta, int idOperacao, decimal valor);
        
        void Pagamento(int idConta, int idOperacao, decimal valor, string codigoBarras);
        
        void Rentabilizacao(int idConta, int idOperacao, decimal taxa);

        IEnumerable<MovimentoDTO> Listar();
        
        MovimentoDTO Obter(int id);
        
        void Inserir(MovimentoDTO movimentoDTO);
        
        void Atualizar(MovimentoDTO movimentoDTO);
        
        void Excluir(int id);
    }
}