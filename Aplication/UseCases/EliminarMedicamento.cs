using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class EliminarMedicamento
    {
        private readonly IMedicamentoRepository _repository;

        public EliminarMedicamento(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task EjecutarAsync(Guid id)
        {
            await _repository.EliminarAsync(id);
        }
    }
}