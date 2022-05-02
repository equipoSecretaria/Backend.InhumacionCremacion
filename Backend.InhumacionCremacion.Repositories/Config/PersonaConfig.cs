using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    /// <summary>
    /// PersonaConfig
    /// </summary>
    public static class PersonaConfig
    {
        /// <summary>
        /// Adds the persona.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddPersona(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("Persona", "inhumacioncremacion");

                entity.Property(e => e.IdPersona).ValueGeneratedNever();

                entity.Property(e => e.FechaNacimiento)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdEstadoCivil).HasColumnName("IdEstadoCIvil");

                entity.Property(e => e.Nacionalidad)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.SegundaNacionalidad)
                   .IsRequired()
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.NumeroIdentificacion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OtroParentesco)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerApellido)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrimerNombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoApellido)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SegundoNombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSolicitudNavigation)
                    .WithMany(p => p.Persona)
                    .HasForeignKey(d => d.IdSolicitud)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Persona_Solicitud");

                //entity.HasOne(d => d.IdUbicacionPersonaNavigation)
                //    .WithMany(p => p.Persona)
                //    .HasForeignKey(d => d.IdUbicacionPersona)
                //    .HasConstraintName("FK_Persona_UbicacionPersona");
            });
        }
    }
}
