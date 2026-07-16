using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class Logs
    {
        public virtual string Id { get; set; }
        public virtual string Descrip { get; set; }
        public virtual DateTime Fechahora { get; set; }
        public virtual Users User { get; set; }

        public virtual ProfesionalesUsers Profesional { get; set; }
    }
}