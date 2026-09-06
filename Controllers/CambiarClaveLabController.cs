using System.Web.Mvc;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class CambiarClaveLabController : Controller
    {
        IProfesionalesUsersService entityService = (IProfesionalesUsersService)SpringContext.Instance.GetObject("ProfesionalesUsersService");
        ProfesionalesUsers entity;

        [AllowAnonymous]
        public ActionResult Index()
        {
            entity = new ProfesionalesUsers();
            return View("Index", entity);
        }

        [HttpPost]
        public string verify(ProfesionalesUsers pojo)
        {
            
            if (Session["__id"] == null) return "ERROR";
            if (pojo.Pass1 == null) return "ERROR";

            entity = entityService.find(Session["__id"].ToString());

            if (entity == null) return "ERROR";

            if (!pojo.Pass1.Equals(entity.pass)) return "ERROR";

            return "OK";
        }

        [HttpPost]
        public string change(ProfesionalesUsers pojo)
        {

            if (Session["__id"] == null) return "ERROR inicio sesión";
            if (pojo.Pass3 == null || pojo.Pass3 == null) return "ERROR las claves son nulas";
            if (!pojo.Pass3.Equals(pojo.Pass1)) return "ERROR no coinciden las claves";

            entity = entityService.find(Session["__id"].ToString());

            entity.pass = pojo.Pass3;

            entityService.update(entity);

            return "OK";
        }

    }
}