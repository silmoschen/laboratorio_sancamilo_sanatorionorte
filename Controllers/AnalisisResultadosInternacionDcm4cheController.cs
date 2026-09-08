using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using laboratoriobioquimico.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class AnalisisResultadosInternacionDcm4cheController : Controller
    {
        IAnalisisSolicitudesInternacionService entityService = (IAnalisisSolicitudesInternacionService)SpringContext.Instance.GetObject("AnalisisSolicitudesInternacionService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");
        IParametrosService parametroService = (IParametrosService)SpringContext.Instance.GetObject("ParametrosService");
        Utiles utiles = new Utiles();

        public ActionResult Index()
        {
            if (!Dcm4cheHabilitado()) return RedirectToAction("Index", "Home");

            ModelFiltro entity = new ModelFiltro();
            entity.desde = DateTime.Now;
            entity.hasta = DateTime.Now;

            if (Session["error"] != null) ViewBag.error = Session["error"];
            Session["error"] = null;

            return View(entity);
        }

        [HttpPost]
        public ActionResult Index(ModelFiltro pojo)
        {
            if (!Dcm4cheHabilitado()) return RedirectToAction("Index", "Home");

            CargarLista(pojo);

            if (Session["error"] != null) ViewBag.error = Session["error"];
            Session["error"] = null;

            return View(pojo);
        }

        [HttpPost]
        public ActionResult Enviar(ModelFiltro pojo)
        {
            if (!Dcm4cheHabilitado()) return RedirectToAction("Index", "Home");

            int enviados = 0;
            int errores = 0;

            Parametros parametro = parametroService.find(1);
            if (parametro == null || string.IsNullOrEmpty(parametro.Titulo1) || string.IsNullOrEmpty(parametro.Titulo3))
            {
                ViewBag.error = "ERROR: parámetros de dcm4che incompletos";
                CargarLista(pojo);
                return View("Index", pojo);
            }

            if (pojo.ListProtocolosInternacion != null)
            {
                foreach (AnalisisSolicitudesInternacion item in pojo.ListProtocolosInternacion)
                {
                    if (item == null || !item.dcm4che || string.IsNullOrEmpty(item.nrosolicitud)) continue;

                    string estado = EnviarUno(item.nrosolicitud, parametro);
                    if (estado != null && estado.StartsWith("ERROR")) errores++;
                    else enviados++;
                }
            }

            CargarLista(pojo);

            ViewBag.error = "Proceso finalizado. Enviados: " + enviados + ". Con error: " + errores + ".";

            return View("Index", pojo);
        }

        private bool Dcm4cheHabilitado()
        {
            Parametros parametro = parametroService.find(1);
            return parametro != null && parametro.Opt1;
        }

        private void CargarLista(ModelFiltro pojo)
        {
            string[] l = { "fecha", "nrosolicitud" };
            IList<AnalisisSolicitudesInternacion> list = entityService.getListProtocolosFecha(
                pojo.desde.ToString("yyyyMMdd"),
                pojo.hasta.ToString("yyyyMMdd"),
                l);

            pojo.ListProtocolosInternacion = new List<AnalisisSolicitudesInternacion>(list);
            foreach (AnalisisSolicitudesInternacion o in pojo.ListProtocolosInternacion)
            {
                o.logsDcm4che = entityService.getListSenddcm4che(o.nrosolicitud);
            }
        }

        private string EnviarUno(string nrosolicitud, Parametros parametro)
        {
            string estado = "";
            AnalisisSolicitudesInternacion entity = entityService.find(nrosolicitud);
            if (entity == null) return "ERROR: protocolo no encontrado";

            string patientDni = "";
            if (entity.paciente != null && !string.IsNullOrEmpty(entity.paciente.nrodoc))
                patientDni = entity.paciente.nrodoc.Trim();

            if (string.IsNullOrEmpty(patientDni))
            {
                estado = "ERROR: número de documento requerido";
                GuardarLog(entity, patientDni, "ERROR", estado, estado);
                return estado;
            }

            string patientName = (entity.paciente != null && entity.paciente.nombre != null) ? entity.paciente.nombre : "";
            string studyDate = FormatStudyDate(entity.fecha);
            string studyType = "LAB";

            try
            {
                foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;
                var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);
                Session["__entitiesreport"] = list;

                string pdfPath = Server.MapPath("/work/pdf/" + nrosolicitud + ".pdf");
                byte[] pdfBytes = PrintReportController.RenderAndSavePdf(Server, list, "ReportProtocoloResult", nrosolicitud);

                string url = parametro.Titulo1;
                string apiKey = parametro.Titulo3;

                HttpStatusCode statusCode;
                string detalle;
                PostDcm4cheMultipart(url, apiKey, nrosolicitud, patientDni, patientName, studyDate, studyType, pdfBytes, Path.GetFileName(pdfPath), out statusCode, out detalle);

                if (string.IsNullOrEmpty(detalle)) detalle = statusCode.ToString();
                if (detalle.Length > 255) detalle = detalle.Substring(0, 255);

                if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.Ambiguous)
                    estado = "Enviado Correctamente a dcm4che";
                else if (statusCode == HttpStatusCode.NotFound)
                    estado = "ERROR: no se encontró el destino o el paciente en dcm4che";
                else
                    estado = "ERROR: " + statusCode + " " + detalle;

                if (estado.Length > 255) estado = estado.Substring(0, 255);

                GuardarLog(entity, patientDni, statusCode.ToString(), detalle, estado);
            }
            catch (Exception ex)
            {
                estado = "ERROR: " + ex.Message;
                string detalle = ex.Message;
                if (detalle != null && detalle.Length > 255) detalle = detalle.Substring(0, 255);
                if (estado.Length > 255) estado = estado.Substring(0, 255);

                GuardarLog(entity, patientDni, "ERROR", detalle, estado);
            }

            return estado;
        }

        private void GuardarLog(AnalisisSolicitudesInternacion entity, string patientDni, string opt2, string detalle, string estado)
        {
            AnalisisSolicitudesInternacionSenddcm4che log = new AnalisisSolicitudesInternacionSenddcm4che();
            log.id = utiles.guiid();
            log.fechahora = DateTime.Now;
            log.solicitud = entity;
            log.opt1 = patientDni;
            log.opt2 = opt2;
            log.opt3 = detalle;
            log.estado = estado;
            entityService.saveSenddcm4che(log);
        }

        private void PostDcm4cheMultipart(string url, string apiKey, string externalId, string patientDni, string patientName, string studyDate, string studyType, byte[] pdfBytes, string fileName, out HttpStatusCode statusCode, out string detalle)
        {
            string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.KeepAlive = true;
            request.Headers.Add("X-API-Key", apiKey);
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            using (Stream requestStream = request.GetRequestStream())
            {
                WriteFormField(requestStream, boundary, "externalId", externalId);
                WriteFormField(requestStream, boundary, "patientDni", patientDni);
                WriteFormField(requestStream, boundary, "patientName", patientName ?? "");
                WriteFormField(requestStream, boundary, "studyDate", studyDate ?? "");
                WriteFormField(requestStream, boundary, "studyType", studyType);

                byte[] fileHeader = Encoding.UTF8.GetBytes(
                    "--" + boundary + "\r\n" +
                    "Content-Disposition: form-data; name=\"files\"; filename=\"" + fileName + "\"\r\n" +
                    "Content-Type: application/pdf\r\n\r\n");
                requestStream.Write(fileHeader, 0, fileHeader.Length);
                requestStream.Write(pdfBytes, 0, pdfBytes.Length);

                byte[] footer = Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
                requestStream.Write(footer, 0, footer.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    statusCode = response.StatusCode;
                    detalle = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    statusCode = errorResponse.StatusCode;
                    using (StreamReader reader = new StreamReader(errorResponse.GetResponseStream()))
                        detalle = reader.ReadToEnd();
                }
                else
                {
                    throw;
                }
            }
        }

        private void WriteFormField(Stream stream, string boundary, string name, string value)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(
                "--" + boundary + "\r\n" +
                "Content-Disposition: form-data; name=\"" + name + "\"\r\n\r\n" +
                value + "\r\n");
            stream.Write(bytes, 0, bytes.Length);
        }

        private string FormatStudyDate(string fecha)
        {
            if (string.IsNullOrEmpty(fecha)) return "";
            if (fecha.Length >= 8 && fecha.IndexOf('-') < 0 && fecha.IndexOf('/') < 0)
                return fecha.Substring(0, 4) + "-" + fecha.Substring(4, 2) + "-" + fecha.Substring(6, 2);
            return fecha;
        }
    }
}
