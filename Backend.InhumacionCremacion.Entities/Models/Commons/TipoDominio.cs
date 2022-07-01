using System;
using System.Collections.Generic;

namespace Backend.InhumacionCremacion.Entities.Models.Commons

{
    public partial class TipoDominio
    {
        public TipoDominio()
        {
            Dominio = new HashSet<Dominio>();
        }

        public Guid Id { get; set; }
        public string Descripcion { get; set; }
        public long Estado { get; set; }

        public virtual ICollection<Dominio> Dominio { get; set; }
    }
}
