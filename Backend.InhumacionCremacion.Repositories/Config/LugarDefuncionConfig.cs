using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// LugarDefuncionConfig
    /// </summary>
    public static class LugarDefuncionConfig
    {
        /// <summary>
        /// Adds the lugar defuncion.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddLugarDefuncion(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LugarDefuncion>(entity =>
            {
                entity.HasKey(e => e.IdLugarDefuncion)
                    .HasName("PK_UbicacionMuerte");

                entity.ToTable("LugarDefuncion", "inhumacioncremacion");

                entity.Property(e => e.IdLugarDefuncion).ValueGeneratedNever();
            });
        }
    }
}
