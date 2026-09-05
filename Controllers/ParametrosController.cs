using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class ParametrosController : Controller
    {
        IParametrosService entityService = (IParametrosService)SpringContext.Instance.GetObject("ParametrosService");
        Parametros entity = null;
                
        public ActionResult Index()
        {
            entity = entityService.find(1);
            if (entity == null) entity = new Parametros();
            entity.Id = 1;
            return View("Index", entity);
        }

        [HttpPost]
        [UpdateFilter]
        public ActionResult Save(Parametros pojo)
        {
            try
            {
                entityService.persist(pojo);
                return View("Index");
            }
            catch
            {
                return View("Index");
            }
        }

    }
}
