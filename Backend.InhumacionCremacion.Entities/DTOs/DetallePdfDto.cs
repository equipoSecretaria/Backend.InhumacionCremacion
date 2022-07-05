using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class DetallePdfDto
    {
        public string FechaActual { get; set; }
        public string Hora { get; set; }
        public string NumeroLicencia { get; set; }
        public string CertificadoDefuncion { get; set; }
        public string Funeraria { get; set; }
        public string FullNameTramitador { get; set; }
        public string FullNameFallecido { get; set; }
        public string FullNameMedico { get; set; }
        public DateTime FechaFallecido { get; set; }
        public int Edad { get; set; }
        //public string Name { get; set; }
    }
}
