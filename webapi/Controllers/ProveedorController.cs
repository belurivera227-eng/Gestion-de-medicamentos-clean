using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;
using Aplication.DTOs;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedorController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProveedorController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var proveedores = await _context.Proveedores.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ProveedorDTO>>(proveedores));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProveedorDTO dto)
        {
            var proveedor = _mapper.Map<Proveedor>(dto);
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return Ok("Proveedor registrado con éxito");
        }
    }
}