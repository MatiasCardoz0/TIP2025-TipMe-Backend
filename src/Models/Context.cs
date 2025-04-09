using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TipMeBackend.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Mesa> Mesa { get; set; }
        public DbSet<Propina> Propina { get; set; }
        public DbSet<EstadoMesa> Estado { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Agrego los modelos de las tabals
            modelBuilder.Entity<Mesa>().ToTable("Mesa").HasKey(m => m.Id);
            modelBuilder.Entity<Propina>().ToTable("Propina").HasKey(m => m.Id);
            modelBuilder.Entity<EstadoMesa>().ToTable("Estado_Mesa").HasKey(m => m.Id);

            modelBuilder.Entity<Mesa>().Property(m => m.Id).HasColumnName("MESA_ID");
            modelBuilder.Entity<Mesa>().Property(m => m.Nombre).HasColumnName("MESA_NOMBRE");
            modelBuilder.Entity<Mesa>().Property(m => m.Numero).HasColumnName("MESA_NUMERO");
            modelBuilder.Entity<Mesa>().Property(m => m.MozoId).HasColumnName("MESA_MOZO");
            modelBuilder.Entity<Mesa>().Property(m => m.QR).HasColumnName("MESA_QR");
            modelBuilder.Entity<Mesa>().Property(m => m.Estado).HasColumnName("MESA_ESTADO");            

            modelBuilder.Entity<Propina>().Property(m => m.Id).HasColumnName("PROP_ID");
            modelBuilder.Entity<Propina>().Property(m => m.Monto).HasColumnName("PROP_MONTO");
            modelBuilder.Entity<Propina>().Property(m => m.Fecha).HasColumnName("PROP_FECHA");
            modelBuilder.Entity<Propina>().Property(m => m.IdMesa).HasColumnName("PROP_ID_MESA");
            modelBuilder.Entity<Propina>().Property(m => m.IdMozo).HasColumnName("PROP_ID_MOZO");

            modelBuilder.Entity<EstadoMesa>().Property(m => m.Id).HasColumnName("ESTADO_ID");
            modelBuilder.Entity<EstadoMesa>().Property(m => m.Nombre).HasColumnName("ESTADO_NOMBRE");
            modelBuilder.Entity<EstadoMesa>().Property(m => m.Descripcion).HasColumnName("ESTADO_DESCRIPCION");
        }
    }
}
