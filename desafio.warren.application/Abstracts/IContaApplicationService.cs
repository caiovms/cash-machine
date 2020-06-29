using desafio.warren.application.dto;
using System.Collections.Generic;

namespace desafio.warren.application.Abstracts
{
    public interface IContaApplicationService
    {
        IEnumerable<ContaDTO> Listar();

        ContaDTO Obter(int id);

        void Inserir(ContaDTO contaDTO);

        void Atualizar(ContaDTO contaDTO);

        void Excluir(int id);
    }
}
