using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Proveedor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty; // Identificación tributaria
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string ContactoVendedor { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
        public ICollection<Lote> Lotes { get; set; } = new List<Lote>();
    }
}