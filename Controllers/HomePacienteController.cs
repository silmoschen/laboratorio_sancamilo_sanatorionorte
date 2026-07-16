using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class HomePacienteController : Controller
    {

        IAnalisisSolicitudesService entityService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");
        IPacientesService pacienteService = (IPacientesService)SpringContext.Instance.GetObject("PacientesService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");
        IAttachFilesAmbulatorioService attachFileService = (IAttachFilesAmbulatorioService)SpringContext.Instance.GetObject("AttachFilesAmbulatorioService");

        AnalisisSolicitudes entity = null;
        Pacientes paciente = null;

        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<AnalisisSolicitudes> _Paginador;
        private IList<AnalisisSolicitudes> entities;

        string[] l = { "fecha", "nrosolicitud" };

        [AllowAnonymous]

        public ActionResult Index(int pagina = 1)
        {
            if (Session["__id"] == null) return RedirectToAction("../Login");

            paciente = pacienteService.find(Session["__id"].ToString());

            entities = entityService.getAll(pagina, _RegistrosPorPagina, paciente, l);

            foreach (AnalisisSolicitudes c in entities)
            {
                // Verificamos si hay archivos Adjuntos
                c.archivosAdjuntos = attachFileService.getAllProtocolo(c.nrosolicitud);
            }

            long _TotalRegistros = entityService.getTotalRegistros(paciente);

            if (_TotalRegistros == 0) return View("Error");

            var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _Paginador = new PaginadorGenerico<AnalisisSolicitudes>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = entities
            };

            if (Session["error"] != null) ViewBag.error = Session["error"];
            Session["error"] = null;

            Session["pagina"] = _Paginador.PaginaActual;

            return View(_Paginador);
        }

        [AllowAnonymous]
        public ActionResult PrintResult2(string id)
        {
            AnalisisSolicitudes entity = entityService.find(id);

            entity.archivosAdjuntos = attachFileService.getAllProtocolo(id);

            foreach (AnalisisSolicitudesItems c in entity.practicas) c.Select = true;

            var list = reportService.getProtocoloResult(entity.nrosolicitud, entity.practicas);

            Session["__entitiesreport"] = list;

            return View("PreviewResult", entity);
        }

        [AllowAnonymous]
        public ActionResult IndexPag()
        {
            int pag = 1;

            if (Session["pagina"] != null) pag = Convert.ToInt32(Session["pagina"].ToString());

            return Redirect("Index?pagina=" + pag.ToString());
        }


    }
}
