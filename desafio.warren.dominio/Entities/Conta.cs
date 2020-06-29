using System;
using System.Collections.Generic;

namespace desafio.warren.domain.Entities
{
    public class Conta
    {
        public Conta()
        {
            Movimentos = new List<Movimento>();
        }

        public int Id { get; set; }
        public string Agencia { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public char Digito { get; set; }
        public decimal Saldo { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Movimento> Movimentos { get; set; }
    }
}