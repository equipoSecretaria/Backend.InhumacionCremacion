using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// SeguimientoConfig
    /// </summary>
    public static class LicenciaConfig
    {
        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddLicencia(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Licencia>(entity =>
            {
                entity.ToTable("Licencia", "inhumacioncremacion");
                entity.HasKey(e => e.ID_Tabla);
                entity.Property(e => e.ID_Tabla).ValueGeneratedNever();
                entity.Property(e => e.ID_Documento).ValueGeneratedNever();
                entity.Property(e => e.FechaGeneracion).HasColumnType("datetime");
            });
        }
    }
}
