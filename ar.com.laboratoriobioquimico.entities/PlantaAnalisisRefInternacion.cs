namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class PlantaAnalisisRefInternacion
    {
        public virtual PlantaAnalisisRefInternacionPK ID { get; set; }
        public virtual string observaciones { get; set; }
        public virtual string observacion { get; set; }
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }
    }

    public class PlantaAnalisisRefInternacionPK
    {
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as PlantaAnalisisRefInternacionPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (codigo + items).GetHashCode();
        }
    }
}