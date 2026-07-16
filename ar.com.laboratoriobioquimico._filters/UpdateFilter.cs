using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico._filters
{
    public class UpdateFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller  
                return;
            }

            // Check for authorization  
            //if (HttpContext.Current.Session["Rol"] == null) return;
            if (HttpContext.Current.Session["Rol"] == null) filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
               new { action = "Index", controller = "Login" }));

            if (!HttpContext.Current.Session["Rol"].ToString().Equals("1")) filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
               new { action = "Index", controller = "ErrorAuthorize" }));

            return;
        }
    }
}