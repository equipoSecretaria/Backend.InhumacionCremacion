using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context.Config.Commons
{
    public static class TipoDominioConfig
    {
        /// <summary>
        /// Adds the tipo dominio.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddTipoDominio(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Models.Commons.TipoDominio>(entity =>
            {
                entity.ToTable("TipoDominio", "commons");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });
        }
    }
}
