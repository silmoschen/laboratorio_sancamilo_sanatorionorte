namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class PacientesMails
    {
        public virtual string id { get; set; }
        public virtual string email { get; set; }
        public virtual Pacientes paciente { get; set; }
    }
}