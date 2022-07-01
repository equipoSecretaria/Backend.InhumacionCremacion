using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class DocumentosSoporteDTO
    {
        //public Guid IdSolicitud { get; set; }
        public Guid IdTipoDocumentoSoporte { get; set; }
        public string Path { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaModificacion { get; set; }
        public Guid IdUsuario { get; set; }
    }
}
