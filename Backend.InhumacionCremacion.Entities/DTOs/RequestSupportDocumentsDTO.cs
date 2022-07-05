using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class RequestSupportDocumentsDTO
    {
        public string IdSolicitud { get; set; }
        public Guid IdTipoDocumentoSoporte { get; set; }
        public string Path { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
