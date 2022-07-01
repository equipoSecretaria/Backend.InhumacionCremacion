using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class Formatos
    {
        public Guid IdPlantilla { get; set; }
        
        public string NombreRegistro { get; set; }
        
        public string AsuntoNotificacion { get; set; }
        
        public string TipoPDF { get; set; }
        
        public string TipoPlantilla { get; set; }
        
        public string valor { get; set; }
        
        public Guid IdTipoPlantilla { get; set; }
        
    }
}