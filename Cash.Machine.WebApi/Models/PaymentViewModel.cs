namespace Cash.Machine.WebApi.Models
{
    public class PaymentViewModel
    {
        public int AccountId { get; set; }
        public int OperationId { get; set; }
        public decimal OperationAmount { get; set; }
        public string BarCode { get; set; }
    }
}