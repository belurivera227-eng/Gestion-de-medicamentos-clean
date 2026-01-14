using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Aplication.DTOs;

namespace Infraestructure.Repositorios
{
    public class MedicamentoRepositorio : IMedicamentoRepository
    {
        private readonly AppDbContext _context;

        public MedicamentoRepositorio(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Medicamento>> ObtenerTodosAsync() 
        {
            return await _context.Medicamentos
                .Include(m => m.Lotes) 
                .ThenInclude(l => l.Proveedor)
                .ToListAsync();
        }
        public async Task<Medicamento?> ObtenerPorIdAsync(Guid id) 
            => await _context.Medicamentos
            .AsNoTracking()
            .Include(m => m.Lotes)
            .FirstOrDefaultAsync(m => m.Id == id);
        public async Task<Medicamento> CrearAsync(Medicamento medicamento)
        {
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();
            return medicamento;
        }
        public async Task ActualizarAsync(Medicamento medicamento)
        {
            _context.Medicamentos.Update(medicamento);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Lote>> ObtenerVencimientosProximosAsync(int dias)
        {
            var fechaLimite = DateTime.Now.AddDays(dias);
            return await _context.Lotes
                .Include(l => l.Medicamento) // Para saber qué medicamento es
                .Where(l => l.FechaVencimiento <= fechaLimite && l.FechaVencimiento >= DateTime.Now)
                .ToListAsync();
        }
        public async Task CrearLoteAsync(Lote lote)
        {
             _context.Lotes.Add(lote);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarAsync(Guid id)
        {
            // Buscamos el medicamento en la base de datos
            var medicamento = await _context.Medicamentos.FindAsync(id);
    
            if (medicamento != null)
            {
            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync(); // Guarda los cambios en SQLALICIA
            }
        }
        public async Task<IEnumerable<object>> ObtenerReporteStockBajoAsync()
{
    return await _context.Medicamentos
        .AsNoTracking() 
        .Select(m => new InventarioDTO 
        {
            Id = m.Id,
            NombreMedicamento = m.Nombre,
            StockMinimo = m.StockMinimo,
            StockActual = m.Lotes.Sum(l => l.Cantidad),
            FechaVencimientoProxima = m.Lotes.Any() 
                ? m.Lotes.Min(l => l.FechaVencimiento) 
                : null 
        })
        .Where(x => x.StockActual < x.StockMinimo) 
        .ToListAsync();
}
        public async Task<IEnumerable<object>> ObtenerInventarioGeneralAsync()
        {
            
            return await _context.Medicamentos
                .Select(m => new InventarioDTO { 
                    Id = m.Id,
                    NombreMedicamento = m.Nombre,
                    StockMinimo = m.StockMinimo,
                    StockActual = m.Lotes.Sum(l => l.Cantidad),
                    FechaVencimientoProxima = m.Lotes.Any() 
                        ? m.Lotes.Min(l => l.FechaVencimiento) 
                        : null
                })
                .ToListAsync();
        }
    }
}