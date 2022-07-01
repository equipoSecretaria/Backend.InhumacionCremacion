using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context.Config.Commons
{
    public static class DominioConfig
    {
        /// <summary>
        /// Adds the dominio.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddDominio(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Models.Commons.Dominio>(entity =>
            {
                entity.ToTable("Dominio", "commons");
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);
                entity.HasOne(d => d.TipoDominioNavigation)
                    .WithMany(p => p.Dominio)
                    .HasForeignKey(d => d.TipoDominio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dominio_TipoDominio");
            });
        }
    }
}
