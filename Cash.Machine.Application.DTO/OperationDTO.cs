using System.Collections.Generic;

namespace Cash.Machine.Application.DTO
{
    public class OperationDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<MovementDTO> Movements { get; set; } = new List<MovementDTO>();
    }
}