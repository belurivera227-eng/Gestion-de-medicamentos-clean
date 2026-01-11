using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using Aplication.DTOs;
using AutoMapper;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IMapper _mapper;

        public MovimientosController(IMovimientoRepository movimientoRepository, IMapper mapper)
        {
            _movimientoRepository = movimientoRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(MovimientoDTO movimientoDto)
        {
            try 
            {
                var movimiento = _mapper.Map<Movimiento>(movimientoDto);
                movimiento.Id = Guid.NewGuid();
                movimiento.Fecha = DateTime.Now;

                await _movimientoRepository.RegistrarMovimientoAsync(movimiento);
        
                return Ok(new { mensaje = "Movimiento registrado y stock actualizado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("historial/{loteId}")]
        public async Task<IActionResult> GetHistorial(Guid loteId)
        {
            var historial = await _movimientoRepository.ObtenerHistorialPorLoteAsync(loteId);
            return Ok(historial);
        }
    }
}