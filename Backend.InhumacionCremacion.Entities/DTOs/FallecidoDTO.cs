using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class FallecidoDTO
    {
        public Guid IdSolicitud { get; set; }
        public string Hora { get; set; }
        public string IdSexo { get; set; }
        public string fechaNacimiento { get; set; }
        public string tipoIdentificacion { get; set; }
        public string edadFallecido { get; set; }
    }
}
