using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;
using System.Web.Http;

namespace laboratoriobioquimico.Controllers
{
    public class RestResultController : ApiController
    {
        IAnalisisSolicitudesService solicitudService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");        
        ModelResult result = null;
        

        [HttpGet, ActionName("solicitudes")]
        public ModelResult getSolicitudes (int id)
        {
            result = new ModelResult();      

	        result.protocolos = solicitudService.getListSolicitudesResults(id);	      

            return result;
        }

        [HttpGet, ActionName("resultado")]
        public ModelResult getSolicitudes(string id)
        {
            result = new ModelResult();

            result.resultados = solicitudService.getSolicitudResults(id);

            return result;
        }
    }
}
