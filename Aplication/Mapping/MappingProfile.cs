using AutoMapper;
using Domain.Entities;
using Aplication.DTOs;

namespace Aplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Medicamentos
            CreateMap<Medicamento, MedicamentoDTO>().ReverseMap();

            // Lotes (Configuración Unificada)
            CreateMap<Lote, LoteDTO>()
                .ForMember(dest => dest.NombreProveedor, 
                           opt => opt.MapFrom(src => src.Proveedor != null ? src.Proveedor.Nombre : "Sin Proveedor"));
            
            // Mapeo inverso para cuando guardas lotes nuevos
            CreateMap<LoteDTO, Lote>();

            // Proveedores
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();
            CreateMap<MovimientoDTO, Movimiento>();
        }
    }
}