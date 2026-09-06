using System.Collections.Generic;
using System.Web.Mvc;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class FacturacionPeriodoController : Controller
    {
        IFacturacionesProfesionalService entityService = (IFacturacionesProfesionalService)SpringContext.Instance.GetObject("FacturacionesProfesionalService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");

        ParametrosDetalleFact parametros = null;
        public ActionResult Index()
        {
            var list = entityService.getProfesionales();
            if (list == null)
            {
                ViewBag.error = "No Hay Profesionales Definidos";
                return View();
            }

            if (Session["parametros"] != null) parametros = (ParametrosDetalleFact)Session["parametros"]; else parametros = new ParametrosDetalleFact();

            parametros.profesionales = list;

            var entities = entityService.getFacturaciones(parametros);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (FacturacionDetalleFact c in entities)
                items.Add(new SelectListItem { Text = c.periodo, Value = c.periodo });

            ViewData["periodos"] = items;

            return View();
        }

        public string report(ParametrosDetalleFact parametros)
        {
            Session["parametros"] = parametros;

            var entities = entityService.getTotalesFacturados(parametros);

            List<Reports> list = reportService.getFacturacionPeriodo(entities);

            Session["__entitiesreport"] = list;
            
            return "OK";
        }

        public ActionResult Preview()
        {
            return View("Preview");
        }

        public ActionResult Excel()
        {
            return View("Export");
        }

    }

        
}