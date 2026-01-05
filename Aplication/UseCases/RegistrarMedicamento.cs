using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Aplication.UseCases
{
    public class RegistrarMedicamento
    {
        private readonly IMedicamentoRepository _repository;
        public RegistrarMedicamento(IMedicamentoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Medicamento> EjecutarAsync(Medicamento medicamento)
        {
            return await _repository.CrearAsync(medicamento);
        }
    }
}