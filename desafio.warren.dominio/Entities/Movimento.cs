using System;

namespace desafio.warren.domain.Entities
{
    public class Movimento
    {
        public int Id { get; set; }
        public int IdConta { get; set; }
        public int IdOperacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string CodigoBarras { get; set; }
        
        public virtual Conta Conta { get; set; }
        public virtual Operacao Operacao { get; set; }
    }
}