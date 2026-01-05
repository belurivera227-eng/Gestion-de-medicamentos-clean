using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMedicamentoRepository
    {
        Task<IEnumerable<Medicamento>> ObtenerTodosAsync();
        Task<Medicamento?> ObtenerPorIdAsync(Guid id);
        Task<Medicamento> CrearAsync(Medicamento medicamento);
        Task ActualizarAsync(Medicamento medicamento);
        Task CrearLoteAsync(Lote lote);
        Task EliminarAsync(Guid id);
        Task<IEnumerable<Lote>> ObtenerLotesVencimientoProximoAsync(int dias);
    }
}