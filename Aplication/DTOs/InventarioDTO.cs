using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Aplication.DTOs
{
    public class InventarioDTO
    {
        public Guid Id { get; set; }
        public string NombreMedicamento { get; set; } = string.Empty;
        public int StockMinimo { get; set; }
        public int StockActual { get; set; }
        public DateTime? FechaVencimientoProxima { get; set; }
        public string ProveedorNombre { get; set; } = "Sin Proveedor";
    }
}