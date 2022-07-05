using System;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion

{
    public partial class FirmaUsuarios
    {
            public Guid ID_FIrma { get; set; }
            public Guid ID_Usuario { get; set; }
            public string Firma { get; set; }
        
    }
}
