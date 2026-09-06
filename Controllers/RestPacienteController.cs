using System.Web.Http;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.Controllers
{
    public class RestPacienteController : ApiController
    {
        IPacientesService entityService = (IPacientesService)SpringContext.Instance.GetObject("PacientesService");
        IPacientesInternacionService pacinterService = (IPacientesInternacionService)SpringContext.Instance.GetObject("PacientesInternacionService");

        [HttpPost, ActionName("get")]
        public ModelPaciente getPaciente([FromBody]ModelPaciente entity)
        {
            ModelPaciente result = new ModelPaciente();

            Pacientes paciente1 = entityService.findByNrodoc(entity.nrodoc);

            if (paciente1 != null)
            {
                result.codpac = paciente1.codpac;
                result.nrodoc = paciente1.nrodoc;
                result.nombre = paciente1.nombre;

                return result;
            }

            PacientesInternacion paciente2 = pacinterService.findByNrodoc(entity.nrodoc);

            if (paciente2 != null)
            {
                result.codpac = paciente2.codpac;
                result.nrodoc = paciente2.nrodoc;
                result.nombre = paciente2.nombre;

                return result;
            }

            return null;
        }
    }
}
