using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.Controllers
{
    public class RestAnalisisController : ApiController
    {
        IAnalisisSolicitudesService entityService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");
        IAnalisisSolicitudesInternacionService solintService = (IAnalisisSolicitudesInternacionService)SpringContext.Instance.GetObject("AnalisisSolicitudesInternacionService");
        
        [HttpPost, ActionName("analisispaciente")]
        public IList<ModelSolicitud> GetAnalisis([FromBody]ModelPaciente entity)
        {
            var result = new List<ModelSolicitud>();            

            var list1 = entityService.getListSolicitudesPaciente(entity.nrodoc);
            var list2 = solintService.getListSolicitudesPaciente(entity.nrodoc);

            // Fusionamos
            //return null;
            if (list1 != null)
            {
                foreach (AnalisisSolicitudes c in list1)
                {
                    var rs = new ModelSolicitud();
                    rs.fecha = c.fecha;
                    rs.fecha1 = c.fecha1;
                    rs.nrosolicitud = c.nrosolicitud;
                    rs.protocolo = c.protocolo;
                    rs.tipo = c.tipo;
                    rs.paciente = new ModelPaciente();
                    rs.paciente.codpac = c.paciente.codpac;
                    rs.paciente.nombre = c.paciente.nombre;
                    rs.paciente.nrodoc = c.paciente.nrodoc;


                    result.Add(rs);
                }
            }

            if (list2 != null)
            {
                foreach (AnalisisSolicitudesInternacion c in list2)
                {
                    var rs = new ModelSolicitud();
                    rs.fecha = c.fecha;
                    rs.fecha1 = c.fecha1;
                    rs.nrosolicitud = c.nrosolicitud;
                    rs.protocolo = c.protocolo;
                    rs.tipo = c.tipo;
                    rs.paciente = new ModelPaciente();
                    rs.paciente.codpac = c.paciente.codpac;
                    rs.paciente.nombre = c.paciente.nombre;
                    rs.paciente.nrodoc = c.paciente.nrodoc;

                    result.Add(rs);
                }
            }

            return result.OrderByDescending(c => c.fecha).ToList<ModelSolicitud>();

        }

    }
}
