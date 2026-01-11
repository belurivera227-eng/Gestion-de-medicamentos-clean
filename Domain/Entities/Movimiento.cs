using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movimiento
    {
        public Guid Id { get; set; }
        public Guid LoteId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } = "Salida"; // Entrada o Salida
        public string Motivo { get; set; } = string.Empty; // Ej: Venta, Ajuste, Vencido
        public DateTime Fecha { get; set; } = DateTime.Now;
        // Relación con el Lote
        public Lote? Lote { get; set; }
    }
}