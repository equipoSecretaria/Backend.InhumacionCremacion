using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Context.Config.Commons
{
    public static class DepartamentoConfig
    {
        /// <summary>
        /// Adds the dominio.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void AddDepartamento(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Models.Commons.Departamento>(entity =>
            {
                entity.ToTable("Departamento", "commons");
                entity.Property(e => e.IdDepartamento).ValueGeneratedNever();
                entity.HasKey(e => e.IdDepartamento);
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

            });
        }
    }
}
