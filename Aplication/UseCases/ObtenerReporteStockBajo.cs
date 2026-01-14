using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Aplication.UseCases
{
    public class ObtenerReporteStockBajo
    {
        private readonly IMedicamentoRepository _repository;

        public ObtenerReporteStockBajo(IMedicamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<object>> EjecutarAsync()
        {
            return await _repository.ObtenerReporteStockBajoAsync();
        }
    }
}