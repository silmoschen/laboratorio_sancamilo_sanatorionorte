using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ApplicationContext;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class RolesController : Controller
    {
        IRolesService entityService = (IRolesService)SpringContext.Instance.GetObject("RolesService");
        Roles entity = null;
        string error = "";               

        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<Roles> _Paginador;
        private IList<Roles> entities;

        private void loadNiveles()
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nivel 1", Value = "1" },
                new SelectListItem { Text = "Nivel 2", Value = "2" },
                new SelectListItem { Text = "Nivel 3", Value = "3" },
                new SelectListItem { Text = "Nivel 4", Value = "4" },
                new SelectListItem { Text = "Nivel 5", Value = "5" }
            };
            ViewData["niveles"] = items;
        }

        [AllowAnonymous]
        public ActionResult Index(int pagina = 1)
        {            
            if (Request["txtbuscar"] != null)
            {
                if (!Request["txtbuscar"].Equals(""))
                {
                    _Paginador = new PaginadorGenerico<Roles>()
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
            _Paginador = new PaginadorGenerico<Roles>()
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

        [AllowAnonymous]
        public ActionResult Create()
        {
            entity = new Roles();
            loadNiveles();
            return View("Edit", entity);
        }

        [AllowAnonymous]
        public ActionResult Edit(long id)
        {
            entity = entityService.find(id);
            loadNiveles();
            return View("Edit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UpdateFilter]
        public ActionResult Save(Roles pojo)
        {
            if (pojo.Descrip == null) error += "Descripción Requerida ";

            if (!error.Equals(""))
            {
                loadNiveles();
                ViewBag.error = error;
                return View("Edit", entity);
            }

            Session["search"] = pojo.Descrip;

            entityService.persist(pojo);
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
