using System;
using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.Models
{
    public class ModelFiltro
    {
        public DateTime desde { get; set; }
        public DateTime hasta { get; set; }
        public List<AnalisisSolicitudesInternacion> ListProtocolosInternacion { get; set; }

        public ModelFiltro()
        {
            ListProtocolosInternacion = new List<AnalisisSolicitudesInternacion>();
        }
    }
}
