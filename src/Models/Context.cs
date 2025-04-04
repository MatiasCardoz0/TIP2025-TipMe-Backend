using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TipMeBackend.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Mesa> Mesa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mesa>()
                .ToTable("Mesa")
                .HasKey(m => m.Id);

            // Configurar columnas específicas (opcional si los nombres coinciden)
            modelBuilder.Entity<Mesa>().Property(m => m.Id).HasColumnName("MESA_ID");
            modelBuilder.Entity<Mesa>().Property(m => m.Nombre).HasColumnName("MESA_NOMBRE");
            modelBuilder.Entity<Mesa>().Property(m => m.Numero).HasColumnName("MESA_NUMERO");
            modelBuilder.Entity<Mesa>().Property(m => m.MozoId).HasColumnName("MESA_MOZO");
            modelBuilder.Entity<Mesa>().Property(m => m.QR).HasColumnName("MESA_QR");
            modelBuilder.Entity<Mesa>().Property(m => m.Estado).HasColumnName("MESA_ESTADO");
        }
    }
}
