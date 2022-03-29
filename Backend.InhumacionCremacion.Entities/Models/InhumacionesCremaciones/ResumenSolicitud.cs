﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class ResumenSolicitud
    {
        public Guid IdSolicitud { get; set; }

        public Guid NumeroTramite { get; set; }

        public Guid EstadoSolicitud { get; set; }

        public string NumeroLicencia { get; set; }

        public string CorreoSolicitante { get; set; }

        public string CorreoFuneraria { get; set; }

        public string CorreoCementerio { get; set; }
        public string CorreoMedico { get; set; }
        
        public string TipoSeguimiento { get; set; }
        public string NombreSolicitante { get; set; }
        public string ApellidoSolicitante { get; set; }
        public string NumeroDocumentoSolicitante { get; set; }
        public Guid TipoDocumentoSolicitante { get; set; }

    }
}
