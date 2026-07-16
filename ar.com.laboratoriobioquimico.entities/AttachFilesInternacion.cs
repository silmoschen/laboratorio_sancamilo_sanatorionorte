using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AttachFilesInternacion
    {
        public virtual string Id { get; set; }
        public virtual string Archivo { get; set; }
        public virtual DateTime Fecha { get; set; }
        public virtual string Protocolo { get; set; }
        public virtual string Original { get; set; }
    }
}