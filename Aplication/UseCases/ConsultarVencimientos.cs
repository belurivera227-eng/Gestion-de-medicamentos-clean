using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class ConsultarVencimientos
    {
        private readonly IMedicamentoRepository _repository;

        public ConsultarVencimientos(IMedicamentoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Lote>> EjecutarAsync(int diasProximos = 30)
        {
            return await _repository.ObtenerLotesVencimientoProximoAsync(diasProximos);
        }
    }
}