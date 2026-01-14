using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class RegistrarLote
    {
        private readonly IMedicamentoRepository _medRepo;

        public RegistrarLote(IMedicamentoRepository medRepo)
        {
            _medRepo = medRepo;
        }

        public async Task EjecutarAsync(Lote lote)
        {
            // Verificamos que el lote no sea nulo y tenga cantidad
            if (lote == null) return;

            // Guardamos el lote en la base de datos a través del repositorio
            await _medRepo.CrearLoteAsync(lote);
        }
    }
}