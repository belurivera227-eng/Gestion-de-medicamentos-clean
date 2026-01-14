using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Aplication.UseCases;
using Aplication.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
        private readonly ObtenerReporteStockBajo _obtenerReporteStockBajo;
        private readonly ObtenerInventarioGeneral _obtenerInventarioGeneral;
        private readonly IMapper _mapper;

        public MedicamentoController(
            RegistrarMedicamento registrarMedicamento, 
            ConsultarVencimientos consultarVencimientos,
            RegistrarLote registrarLote, 
            ObtenerMedicamentos obtenerMedicamentos,
            ActualizarMedicamento actualizarMedicamento,
            EliminarMedicamento eliminarMedicamento,
            ObtenerReporteStockBajo obtenerReporteStockBajo,
            ObtenerInventarioGeneral obtenerInventarioGeneral,
            IMapper mapper)
        {
            _registrarMedicamento = registrarMedicamento;
            _consultarVencimientos = consultarVencimientos;
            _registrarLote = registrarLote; 
            _obtenerMedicamentos = obtenerMedicamentos;
            _actualizarMedicamento = actualizarMedicamento;
            _eliminarMedicamento = eliminarMedicamento;
            _obtenerReporteStockBajo = obtenerReporteStockBajo;
            _obtenerInventarioGeneral = obtenerInventarioGeneral;
            _mapper = mapper;
        }
        [HttpGet("inventario-completo")]
        public async Task<IActionResult> GetInventarioCompleto()
        {
        try 
        {
            var inventario = await _obtenerInventarioGeneral.EjecutarAsync();
            return Ok(inventario);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        }
        [HttpGet("reporte-stock-bajo")]
        public async Task<IActionResult> GetStockBajo()
        {
        var reporte = await _obtenerReporteStockBajo.EjecutarAsync();
        return Ok(reporte);
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

        [HttpPost("lote")] 
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
        [HttpGet("todos-los-lotes")] // <--- Asegúrate de que esta línea esté presente
public async Task<IActionResult> GetTodosLosLotes()
{
    try 
    {
        var medicamentos = await _obtenerMedicamentos.EjecutarAsync();
        
        var listaLotes = medicamentos.SelectMany(m => m.Lotes.Select(l => new LoteDTO
        {
            Id = l.Id,
            NumeroLote = l.NumeroLote,
            Cantidad = l.Cantidad,
            FechaVencimiento = l.FechaVencimiento,
            MedicamentoId = m.Id,
            NombreMedicamento = m.Nombre 
        })).ToList();

        return Ok(listaLotes);
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
}
    }
}