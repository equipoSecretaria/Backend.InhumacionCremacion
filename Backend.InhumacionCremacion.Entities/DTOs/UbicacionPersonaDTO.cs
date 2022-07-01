using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class UbicacionPersonaDTO
    {
        public Guid? IdUbicacionPersona { get; set; }
        public Guid? IdPaisResidencia { get; set; }
        public Guid? IdDepartamentoResidencia { get; set; }
        public Guid? IdCiudadResidencia { get; set; }
        public Guid? IdLocalidadResidencia { get; set; }
        public Guid? IdAreaResidencia { get; set; }
        public Guid? IdBarrioResidencia { get; set; }
    }
}
