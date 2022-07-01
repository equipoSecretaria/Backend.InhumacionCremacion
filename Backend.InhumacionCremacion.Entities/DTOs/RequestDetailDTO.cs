using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class RequestDetailDTO
    {
        public string NumeroCertificado { get; set; }
        public string FechaSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public Guid CodigoTramite { get; set; }
        public int IdPersonaVentanilla { get; set; }
        public string Tramite { get; set; }
        public string Solicitud { get; set; }
        public Guid IdSolicitud { get; set; }
        
        public string nombre { get; set; }
        
    }
}
