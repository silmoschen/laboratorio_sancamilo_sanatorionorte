namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class ProfesionalesUsers
    {
        public virtual string idprof { get; set; }
        public virtual string user { get; set; }
        public virtual string pass { get; set; }
        public virtual Profesionales profesional { get; set; }


        public virtual string Pass3 { get; set; }
        public virtual string Pass1 { get; set; }
        public virtual string Pass2 { get; set; }
    }
}