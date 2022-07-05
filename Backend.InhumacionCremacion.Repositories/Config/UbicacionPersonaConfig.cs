using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// UbicacionPersonaConfig
    /// </summary>
    public static class UbicacionPersonaConfig
    {
        /// <summary>
        /// Adds the ubicacion persona.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddUbicacionPersona(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UbicacionPersona>(entity =>
            {
                entity.HasKey(e => e.IdUbicacionPersona);

                entity.ToTable("UbicacionPersona", "fetal");

                entity.Property(e => e.IdUbicacionPersona).ValueGeneratedNever();
            });
        }
    }
}
