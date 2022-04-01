using System;
using System.Collections.Generic;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class Solicitud
    {
        public Solicitud()
        {
            Persona = new HashSet<Persona>();
        }

        public Guid IdSolicitud { get; set; }
        public string NumeroCertificado { get; set; }
        public DateTime FechaDefuncion { get; set; }
        public bool? SinEstablecer { get; set; }
        public string Hora { get; set; }
        public Guid IdSexo { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public Guid EstadoSolicitud { get; set; }
        public int IdPersonaVentanilla { get; set; }
        public Guid IdUsuarioSeguridad { get; set; }
        public Guid IdTramite { get; set; }
        public Guid IdLugarDefuncion { get; set; }
        public Guid IdTipoMuerte { get; set; }
        public Guid IdDatosCementerio { get; set; }
       
        public Guid IdInstitucionCertificaFallecimiento { get; set; }
        //verificar no mapeo
        //public Guid IdUbicacionPersona { get; set; }

        public virtual DatosCementerio IdDatosCementerioNavigation { get; set; }
      

        public virtual InstitucionCertificaFallecimiento IdInstitucionCertificaFallecimientoNavigation { get; set;}


        public virtual ICollection<Persona> Persona { get; set; }
    }
}
