using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMovimientoRepository
    {
        Task RegistrarMovimientoAsync(Movimiento movimiento);
        Task<IEnumerable<Movimiento>> ObtenerHistorialPorLoteAsync(Guid loteId);
        Task<IEnumerable<Movimiento>> ObtenerHistorialPorFechaAsync(DateTime inicio, DateTime fin);
    }
}