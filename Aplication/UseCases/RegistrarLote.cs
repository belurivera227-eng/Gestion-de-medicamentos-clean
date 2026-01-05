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
        private readonly IMedicamentoRepository _repository;

        public RegistrarLote(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task EjecutarAsync(Lote lote)
        {
            // Aquí podrías poner reglas, ej: no permitir fechas pasadas
            if (lote.FechaVencimiento < DateTime.Now)
                throw new Exception("No se puede registrar un lote ya vencido.");

            await _repository.CrearLoteAsync(lote);
        }
    }
}