using desafio.warren.application.dto;

namespace desafio.warren.webapi.Models
{
    public class CaixaEletronicoViewModel
    {
        public int IdOperacao { get; set; }
        public decimal? ValorOperacao { get; set; }
        public string CodigoDeBarras { get; set; }
        public ContaDTO Conta { get; set; }
        public ExtratoViewModel Extrato { get; set; }
    }
}