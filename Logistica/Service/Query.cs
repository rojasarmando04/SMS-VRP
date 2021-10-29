using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logistica.Service
{
    public class Query
    {
        public int id { get; set; }
        public string capacidad_total { get; set; }
        public string capacidad_utilizada { get; set; }
        public string ruta_ini { get; set; }
        public string ruta_fin { get; set; }
        public double longitud { get; set; }
        public double latitud { get; set; }
    }
}