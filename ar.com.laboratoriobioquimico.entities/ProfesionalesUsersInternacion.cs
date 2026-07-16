namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class ProfesionalesUsersInternacion
    {
        public virtual string idprof { get; set; }
        public virtual string user { get; set; }
        public virtual string pass { get; set; }
        public virtual ProfesionalesInternacion profesional { get; set; }
    }
}