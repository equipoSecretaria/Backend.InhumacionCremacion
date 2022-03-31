using System;
using System.Collections.Generic;
using System.Text;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    public static class DatosFunerariaConfig
    {
        public static void AddDatosFuneraria(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatosFuneraria>(entity =>
            {
                entity.HasKey(e => e.IdDatosFuneraria);

                entity.ToTable("DatosFuneraria", "inhumacioncremacion");

                entity.Property(e => e.IdDatosFuneraria).ValueGeneratedNever();

                entity.Property(e => e.Funeraria).HasMaxLength(200);

                entity.Property(e => e.Ciudad)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OtroSitio).HasMaxLength(200);
            });
        }

    }
}
