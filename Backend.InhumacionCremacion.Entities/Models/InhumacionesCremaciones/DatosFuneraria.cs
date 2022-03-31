﻿using System;
using System.Collections.Generic;

namespace Backend.InhumacionCremacion.Entities.Models.InhumacionCremacion
{
    public partial class DatosFuneraria
    {
        

        public Guid IdDatosFuneraria { get; set; }
        public bool? EnBogota { get; set; }
        public bool? FueraBogota { get; set; }
        public bool? FueraPais { get; set; }
        public string Funeraria { get; set; }
        public string OtroSitio { get; set; }
        public string Ciudad { get; set; }
        public Guid? IdPais { get; set; }
        public Guid? IdDepartamento { get; set; }
        public Guid? IdMunicipio { get; set; }

        public Guid? IdSolicitud { get; set; }


    }
}
