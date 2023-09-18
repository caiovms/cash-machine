using System;

namespace Cash.Machine.Application.DTO
{
    public class MovementDTO
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int OperationId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string BarCode { get; set; }
        public virtual AccountDTO Account { get; set; }
        public virtual OperationDTO Operation { get; set; }
    }
}