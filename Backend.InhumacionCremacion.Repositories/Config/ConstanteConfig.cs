using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// SeguimientoConfig
    /// </summary>
    public static class ConstanteConfig
    {
        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddConstante(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Constante>(entity =>
            {
                entity.ToTable("Constantes", "inhumacioncremacion");
                entity.HasKey(e => e.idConstante);

                entity.Property(e => e.NombreConstante);
                entity.Property(e => e.valor);
            });
        }
    }
}
