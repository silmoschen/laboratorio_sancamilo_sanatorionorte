namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class PlantaAnalisisRef
    {
        public virtual PlantaAnalisisRefPK ID { get; set; }
        public virtual string observaciones { get; set; }

        public virtual string codigo { get; set; }
        public virtual string items { get; set; }
    }

    public class PlantaAnalisisRefPK
    {
        public virtual string codigo { get; set; }
        public virtual string items { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as PlantaAnalisisRefPK;
            return false;
        }

        public override int GetHashCode()
        {
            return (codigo + items).GetHashCode();
        }
    }
}