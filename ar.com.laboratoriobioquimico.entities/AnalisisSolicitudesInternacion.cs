using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.entities
{
    public class AnalisisSolicitudesInternacion
    {
        public string nrosolicitud { get; set; }
        public string protocolo { get; set; }
        public string fecha { get; set; }
        public string version { get; set; }
        public string transferir { get; set; }
        public string tipo { get { return "I"; } }

        public string emailpara { get; set; }
        public string emailtitulo { get; set; }
        [AllowHtml]
        [UIHint("tinymce_mini")]
        public string emailbody { get; set; }

        public string fecha1 { get {
            if (fecha.Length < 8) return fecha;
            return fecha.Substring(6, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4); 
        } }

        public string tieneResultados
        {
            get
            {
                if (version == null) return "";
                if (version != null)
                    if (version.Length > 0) return "S";
                return "";
            }
        }

        public PacientesInternacion paciente { get; set; }
        public ProfesionalesInternacion profesional { get; set; }
        public ObrasSocialesInternacion obrasocial { get; set; }

        public IList<AnalisisSolicitudesInternacionItems> practicas { get; set; }

        public string codos { get; set; }
        public string codpac { get; set; }
        public string idprof { get; set; }

        public IList<AnalisisSolicitudesInternacionSendResults> logs;
        public IList<AttachFilesInternacion> archivosAdjuntos { get; set; }

        public AnalisisSolicitudesInternacion()
        {
            practicas = new List<AnalisisSolicitudesInternacionItems>();
            archivosAdjuntos = new List<AttachFilesInternacion>();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var t = obj as AnalisisSolicitudesInternacion;
            return false;
        }

        public override int GetHashCode()
        {
            return (nrosolicitud).GetHashCode();
        }        
    }
}