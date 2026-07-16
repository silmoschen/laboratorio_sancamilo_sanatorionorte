namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class PlantaAnalisisInternacion
    {
        public virtual PlantaAnalisisInternacionPK ID { get; set; }
        public virtual string elemento { get; set; }
        public virtual string valoresn { get; set; }
        public virtual string resultado { get; set; }
        public virtual string imputable { get; set; }
        public virtual string distancia { get; set; }
        public virtual string itemsparalelo { get; set; }
        public virtual string formula { get; set; }
        public virtual string observaciones { get; set; }

        public virtual string codigo { get; set; }
        public virtual string items { get; set; }
    }

    public class PlantaAnalisisInternacionPK
    {
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as PlantaAnalisisInternacionPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (codigo + items).GetHashCode();
        }
    }
}