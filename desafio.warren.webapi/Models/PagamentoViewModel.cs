namespace desafio.warren.webapi.Models
{
    public class PagamentoViewModel
    {
        public int IdConta { get; set; }
        public int IdOperacao { get; set; }
        public decimal ValorOperacao { get; set; }
        public string CodigoDeBarras { get; set; }
    }
}