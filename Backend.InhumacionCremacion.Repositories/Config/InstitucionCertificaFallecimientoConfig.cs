using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// InstitucionCertificaFallecimientoConfig
    /// </summary>
    public static class InstitucionCertificaFallecimientoConfig
    {
        /// <summary>
        /// Adds the institucion certifica fallecimiento.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddInstitucionCertificaFallecimiento(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstitucionCertificaFallecimiento>(entity =>
            {
                entity.HasKey(e => e.IdInstitucionCertificaFallecimiento)
                    .HasName("PK_InstitucionFallecimiento");

                entity.ToTable("InstitucionCertificaFallecimiento", "inhumacioncremacion");

                entity.Property(e => e.IdInstitucionCertificaFallecimiento).ValueGeneratedNever();

                entity.Property(e => e.FechaActa).HasColumnType("date");

                entity.Property(e => e.NoFiscal)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroActaLevantamiento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroIdentificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroProtocolo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SeccionalFiscalia)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}