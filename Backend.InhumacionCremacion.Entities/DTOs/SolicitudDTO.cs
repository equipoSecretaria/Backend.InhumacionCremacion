using System;
using System.Collections.Generic;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class SolicitudDTO
    {
        //solicitud
        //se crea en backend

        //tener en cuenta en otro modelo
        public Guid IdSolicitud { get; set; }
        public string NumeroCertificado { get; set; }
        public DateTime FechaDefuncion { get; set; }
        public bool? SinEstablecer { get; set; }
        public string Hora { get; set; }
        public Guid IdSexo { get; set; }
        public Guid EstadoSolicitud { get; set; }
        public int IdPersonaVentanilla { get; set; }
        public Guid IdUsuarioSeguridad { get; set; }
        public Guid IdTramite { get; set; }
        //public Guid IdLugarDefuncion { get; set; }
        public Guid IdTipoMuerte { get; set; }
        //public Guid IdDatosCementerio { get; set; }
        //public Guid IdInstitucionCertificaFallecimiento { get; set; }

        //tener en cuenta en otro modelo
        public DateTime FechaSolicitud { get; set; }
        public virtual List<Entities.DTOs.PersonaDTO> Persona { get; set; }
        public virtual Entities.DTOs.LugarDefuncionDTO LugarDefuncion { get; set; }
        public virtual Entities.DTOs.UbicacionPersonaDTO UbicacionPersona { get; set; }
        public virtual Entities.DTOs.DatosCementerioDTO DatosCementerio { get; set; }
        public virtual Entities.DTOs.InstitucionCertificaFallecimientoDTO InstitucionCertificaFallecimiento { get; set; }
        public virtual Entities.DTOs.ResumenSolicitudDTO ResumenSolicitud { get; set; }
        //public virtual List<Entities.DTOs.DocumentosSoporteDTO> DocumentosSoporte { get; set; }
    }
}
