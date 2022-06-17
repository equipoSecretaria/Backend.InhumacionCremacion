using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion

{
    public partial class Licencia
    {
            public Guid ID_Tabla { get; set; }

            public Guid ID_Documento { get; set; }

            public string NombreDocumento { get; set; }

            public int NumeroTramite { get; set; }

            public int NumeroLicencia { get; set; }

            public DateTime FechaGeneracion { get; set; }

            public string DocumentoBase64 { get; set; }

    }
}
