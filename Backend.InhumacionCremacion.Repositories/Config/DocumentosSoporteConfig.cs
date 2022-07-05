using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// DocumentosSoporteConfig
    /// </summary>
    public static class DocumentosSoporteConfig
    {
        /// <summary>
        /// Adds the documentos soporte.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddDocumentosSoporte(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DocumentosSoporte>(entity =>
            {
                entity.HasKey(e => e.IdDocumentoSoporte);

                entity.ToTable("DocumentosSoporte", "inhumacioncremacion");

                entity.Property(e => e.IdDocumentoSoporte).ValueGeneratedNever();

                entity.Property(e => e.FechaModificacion).HasColumnType("datetime");

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
