using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class DatosCementerioDTO
    {
        //campo adicional update
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
    }
}
