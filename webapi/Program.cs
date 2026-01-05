using Microsoft.EntityFrameworkCore;
using Infraestructure.Data;
using Infraestructure.Repositorios;
using Domain.Interfaces;
using Aplication.Mapping;
using Aplication.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Base de Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepositorio>();

// Inyección de Casos de Uso
builder.Services.AddScoped<RegistrarMedicamento>();
builder.Services.AddScoped<ConsultarVencimientos>();
builder.Services.AddScoped<RegistrarLote>(); 
builder.Services.AddScoped<ObtenerMedicamentos>();
builder.Services.AddScoped<ActualizarMedicamento>();
builder.Services.AddScoped<EliminarMedicamento>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();