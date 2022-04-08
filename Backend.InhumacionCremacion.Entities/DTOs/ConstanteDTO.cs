using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.InhumacionCremacion.Entities.DTOs
{
    public class ConstanteDTO
    {
        public Guid idConstante { get; set; }
        public string NombreConstante { get; set; }
        public string valor { get; set; }
    }
}
