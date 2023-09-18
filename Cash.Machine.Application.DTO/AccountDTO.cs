using System;
using System.Collections.Generic;

namespace Cash.Machine.Application.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Agency { get; set; }
        public string Type { get; set; }
        public string Number { get; set; }
        public char Digit { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual ICollection<MovementDTO> Movements { get; set; } = new List<MovementDTO>();
    }
}