using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class LoteDTO
    {
        public Guid Id { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Guid MedicamentoId { get; set; }
        public string? NombreMedicamento { get; set; }
        public Guid? ProveedorId { get; set; }
        public string? NombreProveedor { get; set; }
    }
}