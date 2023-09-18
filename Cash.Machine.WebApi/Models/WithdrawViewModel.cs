namespace Cash.Machine.WebApi.Models
{
    public class WithdrawViewModel
    {
        public int AccountId { get; set; }
        public int OperationId { get; set; }
        public decimal OperationAmount { get; set; }
    }
}