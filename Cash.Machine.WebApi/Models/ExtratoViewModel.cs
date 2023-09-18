using Cash.Machine.Application.DTO;
using System.Collections.Generic;

namespace Cash.Machine.WebApi.Models
{
    public class ExtratoViewModel
    {
        public List<MovementDTO> Movements { get; set; }
        public decimal Total { get; set; }
    }
}
