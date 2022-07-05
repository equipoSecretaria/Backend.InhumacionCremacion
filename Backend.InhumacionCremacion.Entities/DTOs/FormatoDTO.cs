using System;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class FormatoDTO
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