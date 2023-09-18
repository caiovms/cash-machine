using Cash.Machine.Application.DTO;

namespace Cash.Machine.WebApi.Models
{
    public class CashMachineViewModel
    {
        public int OperationId { get; set; }
        public decimal? OperationAmount { get; set; }
        public string BarCode { get; set; }
        public AccountDTO Account { get; set; }
        public ExtratoViewModel BankStatement { get; set; }
    }
}