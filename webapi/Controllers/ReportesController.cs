using Microsoft.AspNetCore.Mvc;
using Aplication.UseCases;
using System.Text;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly GenerarListaCompras _generarLista;

        public ReportesController(GenerarListaCompras generarLista)
        {
            _generarLista = generarLista;
        }

        [HttpGet("lista-compras/txt")]
        public async Task<IActionResult> DescargarListaTexto()
        {
            var contenido = await _generarLista.EjecutarAsync();
            var archivoBytes = Encoding.UTF8.GetBytes(contenido);
            
            // Esto hace que el navegador lo descargue como un archivo .txt
            return File(archivoBytes, "text/plain", $"ListaCompras_{DateTime.Now:yyyyMMdd}.txt");
        }
    }
}