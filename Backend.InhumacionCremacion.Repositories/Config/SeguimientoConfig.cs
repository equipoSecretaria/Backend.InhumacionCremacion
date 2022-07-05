using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// SeguimientoConfig
    /// </summary>
    public static class SeguimientoConfig
    {
        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddSeguimiento(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seguimiento>(entity =>
            {
                entity.ToTable("Seguimiento", "inhumacioncremacion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Observacion)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);
            });
        }
    }
}
