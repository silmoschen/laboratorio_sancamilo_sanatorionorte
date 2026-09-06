using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesInternacionSenddcm4che
    {
        public virtual string id { get; set; }
        public virtual string opt1 { get; set; }
        public virtual string opt2 { get; set; }
        public virtual string opt3 { get; set; }
        public virtual string estado { get; set; }
        public virtual DateTime fechahora { get; set; }

        public virtual AnalisisSolicitudesInternacion solicitud { get; set; }
    }
}