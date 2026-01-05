using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Aplication.UseCases;
using Aplication.DTOs;
using Domain.Entities;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicamentoController : ControllerBase
    {
        private readonly RegistrarMedicamento _registrarMedicamento;
        private readonly ConsultarVencimientos _consultarVencimientos;
        private readonly RegistrarLote _registrarLote;
        private readonly ObtenerMedicamentos _obtenerMedicamentos;
        private readonly ActualizarMedicamento _actualizarMedicamento;
        private readonly EliminarMedicamento _eliminarMedicamento;
        private readonly IMapper _mapper;

        public MedicamentoController(
            RegistrarMedicamento registrarMedicamento, 
            ConsultarVencimientos consultarVencimientos,
            RegistrarLote registrarLote, 
            ObtenerMedicamentos obtenerMedicamentos,
            ActualizarMedicamento actualizarMedicamento,
            EliminarMedicamento eliminarMedicamento,
            IMapper mapper)
        {
            _registrarMedicamento = registrarMedicamento;
            _consultarVencimientos = consultarVencimientos;
            _registrarLote = registrarLote; 
            _obtenerMedicamentos = obtenerMedicamentos;
            _actualizarMedicamento = actualizarMedicamento;
            _eliminarMedicamento = eliminarMedicamento;
            _mapper = mapper;
        }
        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var medicamentos = await _obtenerMedicamentos.EjecutarAsync();
            var resultado = _mapper.Map<IEnumerable<MedicamentoDTO>>(medicamentos);
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MedicamentoDTO dto)
        {
            var medicamento = _mapper.Map<Medicamento>(dto);
            var resultado = await _registrarMedicamento.EjecutarAsync(medicamento);
            return Ok(_mapper.Map<MedicamentoDTO>(resultado));
        }

        [HttpPost("lote")] // <--- NUEVO MÉTODO PARA REGISTRAR LOTES
        public async Task<IActionResult> PostLote([FromBody] LoteDTO dto)
        {
            var lote = _mapper.Map<Lote>(dto);
            await _registrarLote.EjecutarAsync(lote);
            return Ok("Lote registrado con éxito");
        }

        [HttpGet("vencimientos/{dias}")]
        public async Task<IActionResult> GetVencimientos(int dias)
        {
            var lotes = await _consultarVencimientos.EjecutarAsync(dias);
            var resultado = _mapper.Map<IEnumerable<LoteDTO>>(lotes);
            return Ok(resultado);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MedicamentoDTO dto)
        {
            var medicamento = _mapper.Map<Medicamento>(dto);
            medicamento.Id = id; // Aseguramos que el ID sea el de la URL

            try {
            await _actualizarMedicamento.EjecutarAsync(medicamento);
            return Ok("Medicamento actualizado correctamente");
            }
            catch (Exception ex) {
            return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _eliminarMedicamento.EjecutarAsync(id);
            return Ok("Medicamento eliminado correctamente");
        }
    }
}