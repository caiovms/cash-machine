using desafio.warren.application.dto;
using System.Collections.Generic;

namespace desafio.warren.webapi.Models
{
    public class ExtratoViewModel
    {
        public List<MovimentoDTO> Movimentos { get; set; }
        public decimal Total { get; set; }
    }
}
