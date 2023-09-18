namespace Cash.Machine.WebApi.Models
{
    public class DepositViewModel
    {
        public int AccountId { get; set; }
        public int OperationId { get; set; }
        public decimal OperationAmount { get; set; }
    }
}
