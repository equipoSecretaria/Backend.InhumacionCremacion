using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion;

namespace Backend.InhumacionCremacion.Repositories.Config
{
    public static class ResumenSolicitudConfig
    {
        public static void AddResumenSolicitud(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResumenSolicitud>(entity =>
            {
                entity.ToTable("ResumenSolicitud", "inhumacioncremacion");

                entity.HasKey(e => e.IdSolicitud);

                entity.Property(e => e.NumeroTramite).ValueGeneratedNever();

                entity.Property(e => e.EstadoSolicitud).ValueGeneratedNever();


                entity.Property(e => e.NumeroLicencia)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoSolicitante)
                    .HasMaxLength(100).IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CorreoFuneraria)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoCementerio)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TipoSeguimiento)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.NombreSolicitante)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ApellidoSolicitante)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.NumeroDocumentoSolicitante)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.TipoDocumentoSolicitante).ValueGeneratedNever();
            });
        }
    }
}
