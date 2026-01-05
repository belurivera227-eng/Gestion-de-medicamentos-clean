using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lote
    {
        public Guid Id { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public Guid MedicamentoId { get; set; } 
        public Medicamento? Medicamento { get; set; }
        public Guid? ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }
    }
}