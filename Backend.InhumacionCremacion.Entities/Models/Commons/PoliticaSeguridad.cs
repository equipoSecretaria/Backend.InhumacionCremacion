using System;

namespace Backend.InhumacionCremacion.Entities.Models.Commons
{
    public partial class PoliticaSeguridad
    {
        public DateTime fecha { get; set; }


        public Guid usuario { get; set; }

        public bool estado { get; set; }

        public Guid idPoliticaSeguridad { get; set; }


    }
}
