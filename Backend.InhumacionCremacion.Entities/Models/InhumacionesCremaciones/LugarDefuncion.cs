using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class LugarDefuncion
    {
        public Guid IdLugarDefuncion { get; set; }
        public Guid IdPais { get; set; }
        public Guid IdDepartamento { get; set; }
        public Guid IdMunicipio { get; set; }
        public Guid IdAreaDefuncion { get; set; }
        public Guid IdSitioDefuncion { get; set; }
    }
}
