using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Session["__useramam"] == null) RedirectToAction("Index", "Login");
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "shmSOFT Gestión de Laboratorios";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contacto";

            return View();
        }


    }
}
