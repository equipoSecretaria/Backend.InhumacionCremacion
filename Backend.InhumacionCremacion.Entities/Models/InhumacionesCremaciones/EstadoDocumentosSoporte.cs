using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class EstadoDocumentosSoporte
    {

        public Guid IdEstadoDocumento { get; set; }
        public Guid IdSolicitud { get; set; }

        public Guid IdDocumentoSoporte { get; set; }
        public string Path { get; set; }
        public string Observaciones { get; set; }
        public string Estado_Documento { get; set; }
     
        public Guid? TipoSeguimiento { get; set; }
    }
}
