using System;

namespace Cash.Machine.Domain.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int OperationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string BarCode { get; set; }
        
        public virtual Account Account { get; set; }
        public virtual Operation Operation { get; set; }
    }
}