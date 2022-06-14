using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class EstadoDocumentosSoporteDTO
    {
        public Guid IdEstadoDocumento { get; set; }
        public Guid IdSolicitud { get; set; }

        public Guid IdDocumentoSoporte { get; set; }
        public string Path { get; set; }
        public string Observaciones { get; set; }
        public string Estado_Documento { get; set; }

        public string tipo_documento { get; set; }
        public string fecha_registro { get; set; }
        public string fecha_ultima_modificacion { get; set; }
        public Guid? TipoSeguimiento { get; set; }

        public string tipoSeguimientoDescripcion { get; set; }
    }
}
