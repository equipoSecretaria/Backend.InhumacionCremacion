using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class Persona
    {
        public Guid IdPersona { get; set; }
        public Guid TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Nacionalidad { get; set; }
        public string OtroParentesco { get; set; }
        public bool? Estado { get; set; }
        public Guid? IdEstadoCivil { get; set; }
        public Guid? IdNivelEducativo { get; set; }
        public Guid? IdEtnia { get; set; }
        public Guid? IdRegimen { get; set; }
        public Guid IdTipoPersona { get; set; }
        public Guid IdSolicitud { get; set; }
        public Guid? IdParentesco { get; set; }
        public Guid? IdLugarExpedicion { get; set; }
        public Guid? IdTipoProfesional { get; set; }
        public Guid? IdUbicacionPersona { get; set; }
        public virtual Solicitud IdSolicitudNavigation { get; set; }

        //public string TipoMuerte { get; set; }
       // public string TipoMuerte { get; set; }
        //public virtual UbicacionPersona IdUbicacionPersonaNavigation { get; set; }
    }
}
