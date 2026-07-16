using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesItems
    {
        public virtual AnalisisSolicitudesItemsPK ID { get; set; }
        public Nomenclatura practica { get; set; }
        public Nbu nbu { get; set; }
        public AnalisisSolicitudes solicitud { get; set; }
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

        public virtual IList<AnalisisSolicitudesItemsResultado> resultadoitem { get; set; }

        public string nrosolicitud { get; set; }
        public string items { get; set; }
        public string codigo { get; set; }

        public AnalisisSolicitudesItems()
        {
            resultadoitem = new List<AnalisisSolicitudesItemsResultado>();
        }

    }

    public class AnalisisSolicitudesItemsPK
    {
        public virtual string nrosolicitud { get; set; }
        public virtual string items { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AnalisisSolicitudesItemsPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (nrosolicitud + items).GetHashCode();
        }
    }


}