using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Web.Mvc;
using ApplicationContext;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class CambiarClaveController : Controller
    {
        IUsersService entityService = (IUsersService)SpringContext.Instance.GetObject("UsersService");
        Users entity = null;

        [AllowAnonymous]
        public ActionResult Index()
        {
            entity = new Users();
            return View("Index", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string verify(Users pojo)
        {

            if (Session["__id"] == null) return "ERROR";
            if (pojo.Pass1 == null) return "ERROR";

            entity = entityService.find(Convert.ToInt64(Session["__id"].ToString()));

            if (entity == null) return "ERROR";

            if (!pojo.Pass1.Equals(entity.Pass)) return "ERROR";

            return "OK";
        }

        [HttpPost]
        [UpdateFilter]
        [ValidateAntiForgeryToken]
        public string change(Users pojo)
        {

            if (Session["__id"] == null) return "ERROR inicio sesión";
            if (pojo.Pass == null || pojo.Pass == null) return "ERROR las claves son nulas";
            if (!pojo.Pass.Equals(pojo.Pass1)) return "ERROR no coinciden las claves";

            entity = entityService.find(Convert.ToInt64(Session["__id"].ToString()));

            entity.Pass = pojo.Pass;

            entityService.update(entity);

            return "OK";
        }

    }
}