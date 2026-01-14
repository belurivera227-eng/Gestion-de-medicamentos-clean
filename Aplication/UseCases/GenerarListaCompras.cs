using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class GenerarListaCompras
    {
        private readonly IMedicamentoRepository _medRepo;

        public GenerarListaCompras(IMedicamentoRepository medRepo)
        {
            _medRepo = medRepo;
        }

        public async Task<string> EjecutarAsync()
        {
            var stockBajo = await _medRepo.ObtenerReporteStockBajoAsync();
            
            var sb = new StringBuilder();
            sb.AppendLine("📋 LISTA DE COMPRAS - FARMACIA");
            sb.AppendLine($"Fecha: {DateTime.Now:dd/MM/yyyy}");
            sb.AppendLine("-------------------------------");

            if (!stockBajo.Any())
            {
                sb.AppendLine("✅ Todo el stock está al día.");
            }
            else
            {
                foreach (var item in stockBajo)
                {
                    // Usamos reflexión para sacar el nombre del objeto anónimo
                    var nombre = item.GetType().GetProperty("NombreMedicamento")?.GetValue(item, null);
                    var stockActual = item.GetType().GetProperty("StockActual")?.GetValue(item, null);
                    
                    sb.AppendLine($"- {nombre} (Stock actual: {stockActual})");
                }
            }

            sb.AppendLine("-------------------------------");
            sb.AppendLine("Generado automáticamente por el Sistema de Gestión.");

            return sb.ToString();
        }
    }
}