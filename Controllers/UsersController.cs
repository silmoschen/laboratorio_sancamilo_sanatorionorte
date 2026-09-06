using System.Collections.Generic;
using System.Web.Mvc;
using System;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class UsersController : Controller
    {

        IUsersService entityService = (IUsersService)SpringContext.Instance.GetObject("UsersService");
        Users entity = null;
        string error = "";

        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<Users> _Paginador;
        private IList<Users> entities;

        List<SelectListItem> items;

        private void loadRoles()
        {
            items = new List<SelectListItem>();
            IList<Roles> roles = entityService.getListRoles();
            foreach (Roles r in roles) items.Add(new SelectListItem { Text = r.Descrip, Value = r.Id.ToString() });
            ViewData["roles"] = items;
        }

        [AllowAnonymous]
        public ActionResult Index(int pagina = 1)
        {
            if (Request["txtbuscar"] != null)
            {
                if (!Request["txtbuscar"].Equals(""))
                {
                    _Paginador = new PaginadorGenerico<Users>()
                    {
                        RegistrosPorPagina = _RegistrosPorPagina,
                        TotalPaginas = 1,
                        PaginaActual = pagina,
                        Resultado = entityService.getList(Request["txtbuscar"].ToString(), 0, _RegistrosPorPagina)
                    };
                    _Paginador.TotalRegistros = _Paginador.Resultado.Count;
                    return View(_Paginador);
                }
            }

            entities = entityService.getAll(pagina, _RegistrosPorPagina);

            long _TotalRegistros = entityService.getTotalRegistros();

            var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _Paginador = new PaginadorGenerico<Users>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = entities
            };

            if (Session["error"] != null) ViewBag.error = Session["error"];
            Session["error"] = null;

            return View(_Paginador);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            entity = new Users();
            loadRoles();
            return View("Edit", entity);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Edit(long id)
        {
            entity = entityService.find(id);
            loadRoles();
            return View("Edit", entity);
        }

        [HttpPost]
        [UpdateFilter]
        public ActionResult Save(Users pojo)
        {
            if (pojo.Usuario == null) error += "Usuario Incorrecto ";
            if (pojo.Pass == null) error += "Passweord Incorrecto ";
            if (pojo.Nombre == null) error += "Nombre Incorrecto ";

            if (!error.Equals(""))
            {
                loadRoles();
                ViewBag.error = error;
                return View("Edit.cshtml", entity);
            }

            Session["search"] = pojo.Usuario;

            entityService.persist(pojo);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [UpdateFilter]
        public ActionResult Delete(long id)
        {
            try
            {
                entity = entityService.find(id);
                error = entityService.remove(entity);             
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                Session["error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
