using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.Models
{
    public class ModelResult
    {
        public IList<Solicitudes> protocolos = new List<Solicitudes>();        
        public IList<AnalisisSolicitudesItemsResultado> resultados = new List<AnalisisSolicitudesItemsResultado>();

    }
}