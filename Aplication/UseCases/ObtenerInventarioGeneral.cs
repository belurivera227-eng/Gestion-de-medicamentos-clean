using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Aplication.DTOs;

namespace Aplication.UseCases
{
    public class ObtenerInventarioGeneral
    {
        private readonly IMedicamentoRepository _repository;

        public ObtenerInventarioGeneral(IMedicamentoRepository repository) => _repository = repository;

        public async Task<IEnumerable<InventarioDTO>> EjecutarAsync()
        {
            var resultado = await _repository.ObtenerInventarioGeneralAsync();
            // Cast de object a InventarioDTO para el Controller
            return resultado.Cast<InventarioDTO>();
        }
    }
}