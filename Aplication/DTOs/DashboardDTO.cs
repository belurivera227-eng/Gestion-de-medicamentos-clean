using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class DashboardDTO
    {
        public int TotalMedicamentos { get; set; }
        public List<string> NombresStockBajo { get; set; } = new();
        public List<string> NombresProximosAVencer { get; set; } = new();
        public int MovimientosHoy { get; set; }
        public int VentasEsteMes { get; set; }
        public string ProveedorPrincipal { get; set; } = "Sin datos";
        public decimal ValorTotalInventario { get; set; }
    }
}