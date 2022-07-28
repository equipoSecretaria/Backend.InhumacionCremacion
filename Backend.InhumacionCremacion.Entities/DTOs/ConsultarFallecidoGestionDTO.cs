using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class ConsultarFallecidoGestionDTO
    {
        public Guid TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

        public string FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string SegundaNacionalidad { get; set; }
        public string OtroParentesco { get; set; }

        public Guid IdSolicitud { get; set; }

        public string FechaSolicitud { get; set; }

        public string NumeroCertificado { get; set; }

        public int? ID_Control_Tramite { get; set; }
    }
}
