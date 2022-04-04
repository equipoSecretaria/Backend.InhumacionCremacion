using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class RequestDetailDTOUser
    {

        public string NumeroCertificado { get; set; }
        public string FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
    
      
        public string Tramite { get; set; }
        public string Solicitud { get; set; }
        public Guid IdSolicitud { get; set; }

        public string Apellido { get; set; }

        public string NumeroIdentificacion { get; set; }

        public string Nombre { get; set; }
    }
}
