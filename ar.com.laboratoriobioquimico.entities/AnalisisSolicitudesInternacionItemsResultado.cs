namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesInternacionItemsResultado
    {
        public virtual AnalisisSolicitudesInternacionItemsResultadoPK ID { get; set; }
        public virtual string resultado { get; set; }
        public virtual string valoresn { get; set; }        
        public virtual string observaciones { get; set; }
        public virtual string version { get; set; }
        public virtual string plantillaID { get; set; }

        public virtual AnalisisSolicitudesInternacionItems solicituditem { get; set; }
        public virtual PlantaAnalisisInternacion plantilla { get; set; }
        public virtual string nroanalisis { get; set; }
        public virtual string nrosolicitud { get; set; }
        public virtual string items { get; set; }
        public virtual string codigo { get; set; }

    }

    public class AnalisisSolicitudesInternacionItemsResultadoPK
    {
        public virtual string nrosolicitud { get; set; }
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }
        public virtual string nroanalisis { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AnalisisSolicitudesInternacionItemsResultadoPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (nrosolicitud + codigo + items + nroanalisis).GetHashCode();
        }
    }

}