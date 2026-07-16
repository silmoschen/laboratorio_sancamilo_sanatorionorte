using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using laboratoriobioquimico.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class EstadisticasReportesController : Controller
    {
        IReportsService entityService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");
        
        // GET: Estadisticas
        public ActionResult Index()
        {
            ModelEstadistica entity = new ModelEstadistica();
            entity.desde = DateTime.Now;
            entity.hasta = DateTime.Now;

            if (Session["report"] != null) entity = (ModelEstadistica)Session["report"];

            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cantidad de Pacientes Mensuales - INTERNACIÓN", Value = "1" },
                new SelectListItem { Text = "Cantidad de Pacientes Mensuales - AMBULATORIO", Value = "2" },
                new SelectListItem { Text = "Cantidad de Determinaciones Mensuales - INTERNACIÓN", Value = "3" },
                new SelectListItem { Text = "Cantidad de Determinaciones Mensuales - AMBULATORIO", Value = "4" },
                new SelectListItem { Text = "Cantidad de Determinaciones Mensuales por Obra Social - INTERNACIÓN", Value = "5" },
                new SelectListItem { Text = "Cantidad de Determinaciones Mensuales por Obra Social - AMBULATORIO", Value = "6" }
            };
            ViewData["reportes"] = items;

            return View("Index", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Generar(ModelEstadistica pojo)
        {
            Session["report"] = pojo;


            if (pojo.reporte == 1)
            {
                var list = entityService.getCantidadPacienteInternadosMes(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaPacientesMes";
            }

            if (pojo.reporte == 2)
            {
                var list = entityService.getCantidadPacienteAmbulatorioMes(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaPacientesMes";
            }

            if (pojo.reporte == 3)
            {
                var list = entityService.getListCantidadPracticasInternacion(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaDeterminacionesMes";
            }

            if (pojo.reporte == 4)
            {
                var list = entityService.getListCantidadPracticasAmbulatorio(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaDeterminacionesMes";
            }

            if (pojo.reporte == 5)
            {
                var list = entityService.getListObrasSocialesInternacion(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaDetObraSocialMes";
            }

            if (pojo.reporte == 6)
            {
                var list = entityService.getListObrasSocialesAmbulatorio(pojo.desde, pojo.hasta);
                Session["__entitiesreport"] = list;
                Session["__listreport"] = "EstadisticaDetObraSocialMes";
            }


            return View("PreviewResult");

        }

    }
}