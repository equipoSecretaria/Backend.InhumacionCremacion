using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    public static  class FormatoConfig
    {
                /// <summary>
        /// Adds the persona.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddFormatos(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Formatos>(entity =>
            {
                entity.HasKey(e => e.IdPlantilla);

                entity.ToTable("Formatos", "inhumacioncremacion");

                entity.Property(e => e.IdPlantilla).ValueGeneratedNever();

                entity.Property(e => e.NombreRegistro)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AsuntoNotificacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPDF)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoPlantilla)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.valor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipoPlantilla);



            });
        }
    }




    }
