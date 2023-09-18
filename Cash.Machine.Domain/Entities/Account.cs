using System;
using System.Collections.Generic;

namespace Cash.Machine.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Agency { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public char Digit { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}