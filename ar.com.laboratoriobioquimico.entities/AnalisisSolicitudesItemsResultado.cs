namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesItemsResultado
    {
        public virtual AnalisisSolicitudesItemsResultadoPK ID { get; set; }
        public virtual string resultado { get; set; }
        public virtual string valoresn { get; set; }        
        public virtual string observaciones { get; set; }
        public virtual string version { get; set; }
        public virtual string plantillaID { get; set; }

        public virtual AnalisisSolicitudesItems solicituditem { get; set; }
        public virtual PlantaAnalisis plantilla { get; set; }
        public virtual string nroanalisis { get; set; }
        public virtual string nrosolicitud { get; set; }
        public virtual string items { get; set; }
        public virtual string codigo { get; set; }

    }

    public class AnalisisSolicitudesItemsResultadoPK
    {
        public virtual string nrosolicitud { get; set; }
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }
        public virtual string nroanalisis { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AnalisisSolicitudesItemsResultadoPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (nrosolicitud + codigo + items + nroanalisis).GetHashCode();
        }
    }

}