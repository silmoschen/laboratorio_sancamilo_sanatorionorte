using System;
using System.Web.Mvc;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using ApplicationContext;

namespace laboratoriobioquimico.Controllers
{
    public class LoginController : Controller
    {
        IUsersService entityService = (IUsersService)SpringContext.Instance.GetObject("UsersService");
        IProfesionalesUsersService userService = (IProfesionalesUsersService)SpringContext.Instance.GetObject("ProfesionalesUsersService");
        ILogsService logService = (ILogsService)SpringContext.Instance.GetObject("LogsService");
        IParametrosService parametrosService = (IParametrosService)SpringContext.Instance.GetObject("ParametrosService");
        IPacientesService pacienteService = (IPacientesService)SpringContext.Instance.GetObject("PacientesService");
        Users entity = null;
        Logs log = null;
        static Utiles utiles= new Utiles();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {           

            Session["__useramam"] = null;
            Session["__initam"] = "ok";
            entity = new Users();

            Parametros c = parametrosService.find(1);
            entity.Entidad = c.Parametro1;
            entity.EntidadPie = c.Texto2;

            return View("Index", entity);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Users pojo)
        {
            entity = entityService.getUser(pojo.Usuario, pojo.Pass);

            Parametros c = parametrosService.find(1);
            pojo.Entidad = c.Parametro1;           

            // Log general

            if (entity != null)
            {
                Session["__useramam"] = entity.Usuario;
                Session["_useram"] = entity.Pass;
                Session["_sessionam"] = entity;

                Session["__id"] = entity.Id;

                Session["UserNameam"] = entity.Rol.Descrip;
                Session["Rol"] = entity.Rol.Id;
                Session["__initam"] = "ok";

                log = new Logs();
                log.Id = utiles.guiid();
                log.User = entity;
                log.Descrip = "Login " + entity.Usuario;
                log.Fechahora = DateTime.Now;

                logService.persist(log);

                return RedirectToAction("../Home");
            }

            // Log profesional 

            ProfesionalesUsers user = userService.findUser(pojo.Usuario, pojo.Pass);

            if (user != null)
            {
                Session["__useramam"] = pojo.Usuario;
                Session["_useram"] = pojo.Pass;
                Session["_sessionam"] = user;

                Session["__id"] = user.idprof;
                                
                Session["Rol"] = 2;
                Session["__initam"] = "ok";

                log = new Logs();
                log.Id = utiles.guiid();
                log.Profesional = user;
                log.Descrip = "Login " + user.profesional.nombre;
                log.Fechahora = DateTime.Now;

                logService.persist(log);

                return RedirectToAction("../Home");
            }

            // Log paciente

            Pacientes paciente = pacienteService.findByNrodoc(pojo.Usuario);

            if (c.Parametro11 != null)
            {
                if (paciente != null && pojo.Pass.Equals(c.Parametro11))
                {
                    Session["__useramam"] = pojo.Usuario;
                    Session["_useram"] = pojo.Pass;
                    Session["_sessionam"] = user;

                    Session["__id"] = paciente.codpac;

                    Session["Rol"] = 3;
                    Session["__initam"] = "ok";

                    log = new Logs();
                    log.Id = utiles.guiid();
                    log.Profesional = user;
                    log.Descrip = "Login Paciente " + paciente.nombre;
                    log.Fechahora = DateTime.Now;

                    logService.persist(log);

                    return RedirectToAction("../HomePaciente");
                }
            }

            ViewBag.error = "Usuario Incorrecto";            

            return View("Index", pojo);
        }

        public ActionResult Logout()
        {
            Session["__useramam"] = null;
            Session["__initam"] = null;
            Session["_useram"] = null;            

            return View("Index", entity);
        }
    }
}
