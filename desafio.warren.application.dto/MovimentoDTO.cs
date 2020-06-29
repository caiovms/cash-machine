using System;

namespace desafio.warren.application.dto
{
    public class MovimentoDTO
    {
        public int Id { get; set; }
        public int IdConta { get; set; }
        public int IdOperacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string CodigoBarras { get; set; }

        public virtual ContaDTO Conta { get; set; }
        public virtual OperacaoDTO Operacao { get; set; }
    }
}