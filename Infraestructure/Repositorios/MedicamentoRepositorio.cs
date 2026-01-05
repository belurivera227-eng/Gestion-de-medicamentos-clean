using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<Lote>> ObtenerLotesVencimientoProximoAsync(int dias)
        {
            var fechaLimite = DateTime.Now.AddDays(dias);
            return await _context.Lotes
                .Include(l => l.Medicamento)
                .Include(l => l.Proveedor) 
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
    }
}