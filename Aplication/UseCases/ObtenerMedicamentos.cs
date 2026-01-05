using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplication.UseCases
{
    public class ObtenerMedicamentos
    {
        private readonly IMedicamentoRepository _repository;

        public ObtenerMedicamentos(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Medicamento>> EjecutarAsync()
        {
            return await _repository.ObtenerTodosAsync();
        }
    }
}