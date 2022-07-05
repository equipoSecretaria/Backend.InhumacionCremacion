using System;


namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class UbicacionPersona
    {
        //public UbicacionPersona()
        //{
        //    Persona = new HashSet<Persona>();
        //}

        public Guid IdUbicacionPersona { get; set; }
        public Guid? IdPaisResidencia { get; set; }
        public Guid? IdDepartamentoResidencia { get; set; }
        public Guid? IdCiudadResidencia { get; set; }
        public Guid? IdLocalidadResidencia { get; set; }
        public Guid? IdAreaResidencia { get; set; }
        public Guid? IdBarrioResidencia { get; set; }

        //public virtual ICollection<Persona> Persona { get; set; }
    }
}
