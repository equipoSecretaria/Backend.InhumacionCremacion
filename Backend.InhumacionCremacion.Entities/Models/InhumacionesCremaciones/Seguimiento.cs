using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion

{
    public partial class Seguimiento
    {
        public Guid Id { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Guid Usuario { get; set; }
        public Guid Estado { get; set; }
        public Guid IdSolicitud { get; set; }
        public string Observacion { get; set; }
    }
}
