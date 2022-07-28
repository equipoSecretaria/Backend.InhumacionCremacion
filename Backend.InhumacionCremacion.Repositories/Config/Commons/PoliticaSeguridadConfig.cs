using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context.Config.Commons
{
    /// <summary>
    /// SolicitudConfig
    /// </summary>
    public static class PoliticaSeguridadConfig
    {
        /// <summary>
        /// Adds the solicitud.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddPoliticaSeguridad(this ModelBuilder modelBuilder)
        {            

            modelBuilder.Entity<Backend.InhumacionCremacion.Entities.Models.Commons.PoliticaSeguridad>(entity =>
            {
                entity.HasKey(e => e.idPoliticaSeguridad);

                entity.ToTable("PoliticaSeguridad", "commons");

                entity.Property(e => e.idPoliticaSeguridad).ValueGeneratedNever();

                entity.Property(e => e.usuario).ValueGeneratedNever();

                entity.Property(e => e.fecha).HasColumnType("date");

            });
        }
    }
}
