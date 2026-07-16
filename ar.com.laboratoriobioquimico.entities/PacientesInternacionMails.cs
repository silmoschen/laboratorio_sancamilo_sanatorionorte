namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class PacientesInternacionMails
    {
        public virtual string id { get; set; }
        public virtual string email { get; set; }
        public virtual PacientesInternacion paciente { get; set; }
    }
}