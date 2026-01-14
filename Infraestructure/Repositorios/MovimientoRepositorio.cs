using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositorios
{
    public class MovimientoRepositorio : IMovimientoRepository
    {
        private readonly AppDbContext _context;

        public MovimientoRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public async Task RegistrarMovimientoAsync(Movimiento movimiento)
        {
            var lote = await _context.Lotes.FindAsync(movimiento.LoteId);
    
            if (lote != null)
            {
                if (movimiento.Tipo.ToLower() == "salida")
                {
                    if (lote.Cantidad < movimiento.Cantidad)
                    {
                        throw new Exception("No hay suficiente stock en este lote para realizar la salida.");
                    }
                    lote.Cantidad -= movimiento.Cantidad;
                }
                else if (movimiento.Tipo.ToLower() == "entrada")
                {
                    lote.Cantidad += movimiento.Cantidad;
                }
                _context.Lotes.Update(lote);
                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Movimiento>> ObtenerHistorialPorLoteAsync(Guid loteId)
        {
            return await _context.Movimientos
                .Where(m => m.LoteId == loteId)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync();
        }
        public async Task<IEnumerable<Movimiento>> ObtenerHistorialPorFechaAsync(DateTime inicio, DateTime fin)
        {
            return await _context.Movimientos
                .Where(m => m.Fecha >= inicio && m.Fecha < fin)
                .ToListAsync();
        }
    }
}