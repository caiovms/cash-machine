using System.Collections.Generic;

namespace Cash.Machine.Domain.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }

    public enum OperationType : int
    {
        WITHDRAW = 1,
        DEPOSIT = 2,
        PAYMENT = 3,
        MONETIZE = 4,
    }
}