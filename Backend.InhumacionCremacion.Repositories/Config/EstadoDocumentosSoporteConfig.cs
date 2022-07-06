using System;
using System.Collections.Generic;
using System.Text;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;
using Microsoft.EntityFrameworkCore;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    public static class EstadoDocumentosSoporteConfig
    {
        public static void AddEstadoDocumentosSoporte(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstadoDocumentosSoporte>(entity =>
            {
                entity.ToTable("EstadoDocumentosSoporte", "inhumacioncremacion");


                entity.HasKey(e => e.IdEstadoDocumento);

                entity.Property(e => e.IdSolicitud).ValueGeneratedNever();

                entity.Property(e => e.IdDocumentoSoporte).ValueGeneratedNever();

                entity.Property(e => e.TipoSeguimiento);

                entity.Property(e => e.Path)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                         
                entity.Property(e => e.Observaciones)
                    .HasMaxLength(550)
                    .IsUnicode(false);

                entity.Property(e => e.Estado_Documento)
                    .HasMaxLength(100)
                    .IsUnicode(false);


            });
        }
    }
}
