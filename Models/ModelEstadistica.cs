using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.Models
{
    public class ModelEstadistica
    {
       public IList<Estadisticas> data { get; set; }
        public bool tipo { get; set; }
        public int reporte { get; set; }
        public int tiporeporte { get; set; }
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }

        public string fecha1 { get; set; }
        public string fecha2 { get; set; }

        public ModelEstadistica()
        {
            data = new List<Estadisticas>();
        }
    }
}