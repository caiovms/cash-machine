using System.Collections.Generic;

namespace desafio.warren.domain.Entities
{
    public class Operacao
    {
        public Operacao()
        {
            Movimentos = new List<Movimento>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Movimento> Movimentos { get; set; }
    }

    public enum TipoOperacao : int
    {
        SAQUE = 1,
        DEPOSITO = 2,
        PAGAMENTO = 3,
        RENTABILIZACAO = 4,
    }
}