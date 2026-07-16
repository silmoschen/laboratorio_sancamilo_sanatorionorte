namespace laboratoriobioquimico.Models
{
    public class ModelSolicitud
    {
        public string nrosolicitud { get; set; }
        public string protocolo { get; set; }
        public string fecha { get; set; }
        public string fecha1 { get; set; }
        public string version { get; set; }
        public string transferir { get; set; }
        public string tipo { get; set; }

        public ModelPaciente paciente { get; set; }
    }
}