using desafio.warren.application.dto;
using System;
using System.Collections.Generic;

namespace desafio.warren.application.Abstracts
{
    public interface IOperacaoApplicationService
    {
        IEnumerable<OperacaoDTO> Listar();

        OperacaoDTO Obter(int id);

        void Inserir(OperacaoDTO operacaoDTO);

        void Atualizar(OperacaoDTO operacaoDTO);

        void Excluir(int id);
    }
}
