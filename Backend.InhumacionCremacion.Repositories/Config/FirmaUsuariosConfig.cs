using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// SeguimientoConfig
    /// </summary>
    public static class FirmaUsariosConfig
    {
        /// <summary>
        /// AddSeguimiento
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void AddFirmaUsuarios(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirmaUsuarios>(entity =>
            {
                entity.ToTable("FirmaUsuarios", "inhumacioncremacion");
                entity.HasKey(e => e.ID_FIrma);

                entity.Property(e => e.ID_Usuario);
                entity.Property(e => e.Firma);
            });
        }
    }
}
