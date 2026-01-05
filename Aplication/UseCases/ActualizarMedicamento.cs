using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class ActualizarMedicamento
    {
        private readonly IMedicamentoRepository _repository;

        public ActualizarMedicamento(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task EjecutarAsync(Medicamento medicamento)
        {
            var existente = await _repository.ObtenerPorIdAsync(medicamento.Id);
            if (existente == null)
                throw new Exception("El medicamento no existe.");

            await _repository.ActualizarAsync(medicamento);
        }
    }
}