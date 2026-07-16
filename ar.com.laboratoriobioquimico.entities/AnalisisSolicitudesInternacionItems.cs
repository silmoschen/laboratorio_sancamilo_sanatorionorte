using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesInternacionItems
    {
        public virtual AnalisisSolicitudesInternacionItemsPK ID { get; set; }
        public NomenclaturaInternacion practica { get; set; }
        public NbuInternacion nbu { get; set; }
        public AnalisisSolicitudesInternacion solicitud { get; set; }
        public string cargado { get; set; }
        public bool Select { get; set; }

        public string determinacion
        {
            get
            {
                if (nbu != null) return nbu.descrip;
                if (practica != null) return practica.descrip;
                return "";
            }
        }

        public string determinacioncodigo
        {
            get
            {
                if (nbu != null) return nbu.codigo;
                if (practica != null) return practica.codigo;
                return "";
            }
        }

        public virtual IList<AnalisisSolicitudesInternacionItemsResultado> resultadoitem { get; set; }

        public string nrosolicitud { get; set; }
        public string items { get; set; }
        public string codigo { get; set; }

        public AnalisisSolicitudesInternacionItems()
        {
            resultadoitem = new List<AnalisisSolicitudesInternacionItemsResultado>();
        }

    }

    public class AnalisisSolicitudesInternacionItemsPK
    {
        public virtual string nrosolicitud { get; set; }
        public virtual string items { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AnalisisSolicitudesInternacionItemsPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (nrosolicitud + items).GetHashCode();
        }
    }


}