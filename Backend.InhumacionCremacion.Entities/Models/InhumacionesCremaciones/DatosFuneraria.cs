using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public class DatosFuneraria
    {
       


        public Guid IdDatosFuneraria { get; set; }
        public bool? EnBogota { get; set; }
        public bool? FueraBogota { get; set; }
        public bool? FueraPais { get; set; }
        public string Funeraria { get; set; }
        public string OtroSitio { get; set; }
        public string Ciudad { get; set; }
        public Guid? IdPais { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdMunicipio { get; set; }

       
    }
}
