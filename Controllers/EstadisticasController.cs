using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using laboratoriobioquimico.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class EstadisticasController : Controller
    {
        IQueryService entityService = (IQueryService)SpringContext.Instance.GetObject("QueryService");
        private Utiles utiles = new Utiles();
        
        //==================================================================================================

        public IList<Estadisticas> getListPacientesDiaAmbulatorio(ModelEstadistica pojo)
        {
            string desde = utiles.fechaAAAAMMDD(pojo.desde);
            string hasta = utiles.fechaAAAAMMDD(pojo.hasta);
            
            var list = entityService.getListAnalisisAmbulatorioDia(desde, hasta);

            var myList = new List<Estadisticas>();

            foreach (object[] row in list)
            {
                Estadisticas entity = new Estadisticas();
                entity.dia = Convert.ToInt32(row[1].ToString());
                entity.cantidad = Convert.ToInt32(row[0].ToString());
                entity.mes = Convert.ToInt32(row[2].ToString());

                myList.Add(entity);
            }

            return myList;
        }

        public IList<Estadisticas> getListPacientesDiaInternacion(ModelEstadistica pojo)
        {
            string desde = utiles.fechaAAAAMMDD(pojo.desde);
            string hasta = utiles.fechaAAAAMMDD(pojo.hasta);

            var list = entityService.getListAnalisisInternacionDia(desde, hasta);

            var myList = new List<Estadisticas>();

            foreach (object[] row in list)
            {
                Estadisticas entity = new Estadisticas();
                entity.dia = Convert.ToInt32(row[1].ToString());
                entity.cantidad = Convert.ToInt32(row[0].ToString());
                entity.mes = Convert.ToInt32(row[2].ToString());

                myList.Add(entity);
            }

            return myList;
        }

        //--------------------------------------------------------------------------------------------------

        public IList<Estadisticas> getListPacientesDiaAmbulatorioMes(ModelEstadistica pojo)
        {
            string desde = utiles.fechaAAAAMMDD(pojo.desde);
            string hasta = utiles.fechaAAAAMMDD(pojo.hasta);

            var list = entityService.getListAnalisisAmbulatorioMes(desde, hasta);

            var myList = new List<Estadisticas>();

            foreach (object[] row in list)
            {
                Estadisticas entity = new Estadisticas();
                entity.mes = Convert.ToInt32(row[1].ToString());
                entity.cantidad = Convert.ToInt32(row[0].ToString());
                entity.anio = Convert.ToInt32(row[2].ToString());

                myList.Add(entity);
            }

            return myList;
        }

        public IList<Estadisticas> getListPacientesDiaInternacionMes(ModelEstadistica pojo)
        {
            string desde = utiles.fechaAAAAMMDD(pojo.desde);
            string hasta = utiles.fechaAAAAMMDD(pojo.hasta);

            var list = entityService.getListAnalisisInternacionMes(desde, hasta);

            var myList = new List<Estadisticas>();

            foreach (object[] row in list)
            {
                Estadisticas entity = new Estadisticas();
                entity.mes = Convert.ToInt32(row[1].ToString());
                entity.cantidad = Convert.ToInt32(row[0].ToString());
                entity.anio = Convert.ToInt32(row[2].ToString());

                myList.Add(entity);
            }

            return myList;
        }

        //==================================================================================================

        // GET: Estadisticas
        public ActionResult Index()
        {
            ModelEstadistica entity = new ModelEstadistica();
            entity.desde = DateTime.Now;
            entity.hasta = DateTime.Now;

            if (Session["report"] != null) entity = (ModelEstadistica)Session["report"];

            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Pacientes por Día", Value = "1" },
                new SelectListItem { Text = "Pacientes por Mes", Value = "2" }
            };
            ViewData["reportes"] = items;

            items = new List<SelectListItem>
            {
                new SelectListItem { Text = "CURVA", Value = "1" },
                new SelectListItem { Text = "PIE", Value = "2" }
            };
            ViewData["tiporeportes"] = items;

            return View("Index", entity);
        }

        [HttpPost]
        public ActionResult Generar(ModelEstadistica pojo)
        {
            Session["report"] = pojo;

            if (pojo.reporte == 1)
            {  // Pacientes por día

                if (!pojo.tipo)  // Ambulatorio
                {

                    pojo.data = getListPacientesDiaAmbulatorio(pojo);

                    ViewBag.titulo = "Pacientes Ingresados por Día - Ambulatorio";

                    switch (pojo.tiporeporte)
                    {
                        case 1:
                            return View("PacientesDiariosCurva", pojo);
                        case 2:
                            return View("PacientesDiariosPie", pojo);
                        default:
                            return RedirectToAction("Index");
                    }

                }

                //================================================================================

                if (pojo.tipo)  // Internación
                {

                    pojo.data = getListPacientesDiaInternacion(pojo);

                    ViewBag.titulo = "Pacientes Ingresados por Día - Internación";

                    switch (pojo.tiporeporte)
                    {
                        case 1:
                            return View("PacientesDiariosCurvaInternacion", pojo);
                        case 2:
                            return View("PacientesDiariosPieInternacion", pojo);
                        default:
                            return RedirectToAction("Index");
                    }

                }
            }

            //-----------------------------------------------------------------------------------

            if (pojo.reporte == 2)
            {  // Pacientes por mes

                if (!pojo.tipo)  // Ambulatorio
                {

                    pojo.data = getListPacientesDiaAmbulatorioMes(pojo);

                    ViewBag.titulo = "Pacientes Ingresados por Mes - Ambulatorio";

                    switch (pojo.tiporeporte)
                    {
                        case 1:
                            return View("PacientesDiariosCurvaMes", pojo);
                        case 2:
                            return View("PacientesDiariosPieMes", pojo);
                        default:
                            return RedirectToAction("Index");
                    }

                }

                //================================================================================

                if (pojo.tipo)  // Internación
                {

                    pojo.data = getListPacientesDiaInternacionMes(pojo);

                    ViewBag.titulo = "Pacientes Ingresados por Mes - Internación";

                    switch (pojo.tiporeporte)
                    {
                        case 1:
                            return View("PacientesDiariosCurvaInternacionMes", pojo);
                        case 2:
                            return View("PacientesDiariosPieInternacionMes", pojo);
                        default:
                            return RedirectToAction("Index");
                    }

                }
            }


            return RedirectToAction("Index");

        }       
       

    }
}