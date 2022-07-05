using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// DatosCementerioConfig
    /// </summary>
    public static class DatosCementerioConfig
    {
        /// <summary>
        /// Adds the datos cementerio.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddDatosCementerio(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatosCementerio>(entity =>
            {
                entity.HasKey(e => e.IdDatosCementerio);

                entity.ToTable("DatosCementerio", "inhumacioncremacion");

                entity.Property(e => e.IdDatosCementerio).ValueGeneratedNever();

                entity.Property(e => e.Cementerio).HasMaxLength(200);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OtroSitio).HasMaxLength(200);
            });
        }
    }
}
