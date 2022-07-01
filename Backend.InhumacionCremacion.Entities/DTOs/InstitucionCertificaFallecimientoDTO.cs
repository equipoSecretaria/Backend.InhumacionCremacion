using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class InstitucionCertificaFallecimientoDTO
    {
        //nuevo validar
        public Guid IdInstitucionCertificaFallecimiento { get; set; }
        public Guid? TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroProtocolo { get; set; }
        public string NumeroActaLevantamiento { get; set; }
        public DateTime? FechaActa { get; set; }
        public string SeccionalFiscalia { get; set; }
        public string NoFiscal { get; set; }
        public Guid IdTipoInstitucion { get; set; }
    }
}
