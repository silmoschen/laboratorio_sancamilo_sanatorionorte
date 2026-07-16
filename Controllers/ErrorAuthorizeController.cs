using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    public class ErrorAuthorizeController : Controller
    {
        // GET: ErrorAuthorize
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
