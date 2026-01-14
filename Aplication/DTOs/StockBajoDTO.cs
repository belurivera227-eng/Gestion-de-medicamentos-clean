using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplication.DTOs
{
    public class StockBajoDTO
    {
        public string NombreMedicamento { get; set; } = string.Empty;
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public int CantidadFaltante => StockMinimo - StockActual;
    }
}