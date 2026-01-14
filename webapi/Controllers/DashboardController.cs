using Microsoft.AspNetCore.Mvc;
using Aplication.UseCases;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ObtenerDashboard _obtenerDashboard;

        public DashboardController(ObtenerDashboard obtenerDashboard)
        {
            _obtenerDashboard = obtenerDashboard;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resumen = await _obtenerDashboard.EjecutarAsync();
            return Ok(resumen);
        }
    }
}