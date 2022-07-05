using System;
using System.Collections.Generic;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class DatosCementerio
    {
        public DatosCementerio()
        {
            Solicitud = new HashSet<Solicitud>();
        }

        public Guid IdDatosCementerio { get; set; }
        public bool? EnBogota { get; set; }
        public bool? FueraBogota { get; set; }
        public bool? FueraPais { get; set; }
        public string Cementerio { get; set; }
        public string OtroSitio { get; set; }
        public string Ciudad { get; set; }
        public Guid? IdPais { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdMunicipio { get; set; }

        public virtual ICollection<Solicitud> Solicitud { get; set; }
    }
}
