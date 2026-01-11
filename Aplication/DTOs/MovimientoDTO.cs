using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class MovimientoDTO
    {
        public Guid LoteId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } = "Salida"; 
        public string Motivo { get; set; } = string.Empty;
    }
}