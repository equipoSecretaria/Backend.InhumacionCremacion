using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// SolicitudConfig
    /// </summary>
    public static class SolicitudConfig
    {
        /// <summary>
        /// Adds the solicitud.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddSolicitud(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.HasKey(e => e.IdSolicitud);

                entity.ToTable("Solicitud", "inhumacioncremacion");

                entity.Property(e => e.IdSolicitud).ValueGeneratedNever();

                entity.Property(e => e.FechaDefuncion).HasColumnType("date");

                entity.Property(e => e.FechaSolicitud).HasColumnType("datetime");
                entity.Property(e => e.ID_Control_Tramite).UseSqlServerIdentityColumn();

                entity.Property(e => e.Hora)
                    .HasMaxLength(100)
                    .IsUnicode(false);

              
                entity.Property(e => e.NumeroCertificado)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDatosCementerioNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdDatosCementerio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_DatosCementerio");

                entity.HasOne(d => d.IdInstitucionCertificaFallecimientoNavigation)
                    .WithMany(p => p.Solicitud)
                    .HasForeignKey(d => d.IdInstitucionCertificaFallecimiento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitud_InstitucionCertificaFallecimiento");
            });
        }
    }
}
