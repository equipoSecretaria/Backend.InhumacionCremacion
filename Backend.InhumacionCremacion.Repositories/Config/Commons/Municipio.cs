using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context.Config.Commons
{
    public static class MunicipioConfig
    {
        /// <summary>
        /// Adds the dominio.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddMunicipio(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Models.Commons.Municipio>(entity =>
            {
                entity.ToTable("Municipio", "commons");
                entity.Property(e => e.IdMunicipio).ValueGeneratedNever();
                entity.HasKey(e => e.IdMunicipio);
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });
        }
    }
}
