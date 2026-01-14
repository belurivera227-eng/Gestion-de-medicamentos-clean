using Domain.Interfaces;
using Aplication.DTOs;

namespace Aplication.UseCases
{
    public class ObtenerDashboard
    {
        private readonly IMedicamentoRepository _medRepo;
        private readonly IMovimientoRepository _movRepo;

        public ObtenerDashboard(IMedicamentoRepository medRepo, IMovimientoRepository movRepo)
        {
            _medRepo = medRepo;
            _movRepo = movRepo;
        }
        public async Task<DashboardDTO> EjecutarAsync()
{
    var medicamentos = await _medRepo.ObtenerTodosAsync();
    var stockBajoInfo = await _medRepo.ObtenerReporteStockBajoAsync();
    var vencimientosInfo = await _medRepo.ObtenerVencimientosProximosAsync(30);
    
    var inicioMes = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
    var mañana = DateTime.Today.AddDays(1);

    var movimientosHoy = await _movRepo.ObtenerHistorialPorFechaAsync(DateTime.Today, mañana);
    var movimientosMes = await _movRepo.ObtenerHistorialPorFechaAsync(inicioMes, mañana);

    // LÓGICA PARA PROVEEDOR PRINCIPAL mejorada para evitar advertencias
    var todosLosLotes = medicamentos
        .Where(m => m.Lotes != null)
        .SelectMany(m => m.Lotes!)
        .ToList();

    var proveedorPrincipal = todosLosLotes
        .Where(l => l.Proveedor != null)
        .GroupBy(l => l.Proveedor!.Nombre) // Agregamos ! porque ya filtramos los nulos arriba
        .OrderByDescending(g => g.Count())
        .Select(g => g.Key)
        .FirstOrDefault() ?? "No definido";

    return new DashboardDTO
    {
        TotalMedicamentos = medicamentos.Count(),
        NombresStockBajo = stockBajoInfo
            .Select(x => x?.GetType().GetProperty("NombreMedicamento")?.GetValue(x, null)?.ToString() ?? "Sin nombre")
            .ToList(),
        NombresProximosAVencer = vencimientosInfo
            .Select(l => l.Medicamento?.Nombre ?? "Medicamento desconocido")
            .Distinct()
            .ToList(),
        MovimientosHoy = movimientosHoy.Count(),
        VentasEsteMes = movimientosMes.Count(m => m.Tipo == "Salida"),
        ProveedorPrincipal = proveedorPrincipal
    };
}
    }
}