using System.Collections.Generic;

namespace desafio.warren.application.dto
{
    public class OperacaoDTO
    {
        public OperacaoDTO()
        {
            Movimentos = new List<MovimentoDTO>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<MovimentoDTO> Movimentos { get; set; }
    }
}