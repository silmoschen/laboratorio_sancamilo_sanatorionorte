using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class LogsController : Controller
    {

        ILogsService entityService = (ILogsService)SpringContext.Instance.GetObject("LogsService");
        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<Logs> _Paginador;
        private IList<Logs> entities;

        public ActionResult Index(int pagina = 1)
        {
            entities = entityService.getAll(pagina, _RegistrosPorPagina);

            long _TotalRegistros = entityService.getTotalRegistros();

            var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _Paginador = new PaginadorGenerico<Logs>()
            {
                RegistrosPorPagina = _RegistrosPorPagina,
                TotalRegistros = _TotalRegistros,
                TotalPaginas = _TotalPaginas,
                PaginaActual = pagina,
                Resultado = entities
            };

            return View(_Paginador);
        }

    }
}
