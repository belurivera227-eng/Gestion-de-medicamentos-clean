using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infraestructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Medicamento> Medicamentos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lote>()
            .HasOne(l => l.Medicamento)
            .WithMany(m => m.Lotes)
            .HasForeignKey(l => l.MedicamentoId); // Esto obliga a usar el nombre correcto

            modelBuilder.Entity<Proveedor>()
            .HasMany(p => p.Lotes)
            .WithOne(l => l.Proveedor)
            .HasForeignKey(l => l.ProveedorId);
        }

    }
}