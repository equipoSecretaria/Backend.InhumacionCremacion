using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class SeguimientoDto
    {
        public Guid Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string Estado { get; set; }
        public Guid IdSolicitud { get; set; }
        public string Observacion { get; set; }
    }
}
