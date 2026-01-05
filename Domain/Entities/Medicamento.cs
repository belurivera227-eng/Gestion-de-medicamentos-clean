using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Medicamento
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty; 
        public string Descripcion { get; set; } = string.Empty;
        public int StockMinimo { get; set; } 
        public bool Estado { get; set; } = true;
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}