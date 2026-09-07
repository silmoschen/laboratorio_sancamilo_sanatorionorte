using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class AnalisisResultadosInternacionController : Controller
    {
        IAnalisisSolicitudesInternacionService entityService = (IAnalisisSolicitudesInternacionService)SpringContext.Instance.GetObject("AnalisisSolicitudesInternacionService");
        IPlantaAnalisisInternacionService templateService = (IPlantaAnalisisInternacionService)SpringContext.Instance.GetObject("PlantaAnalisisInternacionService");
        IPlantaAnalisisRefInternacionService templateRefService = (IPlantaAnalisisRefInternacionService)SpringContext.Instance.GetObject("PlantaAnalisisRefInternacionService");
        IPacientesInternacionService pacienteService = (IPacientesInternacionService)SpringContext.Instance.GetObject("PacientesInternacionService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");
        IParametrosService parametroService = (IParametrosService)SpringContext.Instance.GetObject("ParametrosService");
        IAttachFilesInternacionService attachFileService = (IAttachFilesInternacionService)SpringContext.Instance.GetObject("AttachFilesInternacionService");

        Utiles utiles = new Utiles();
        AnalisisSolicitudesInternacion entity = null;

        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<AnalisisSolicitudesInternacion> _Paginador;
        private IList<AnalisisSolicitudesInternacion> entities;

        string[] l = { "fecha", "nrosolicitud" };

        [AllowAnonymous]
        public ActionResult Index(int pagina = 1)
        {

            entities = entityService.getAll(pagina, _RegistrosPorPagina, l);

            long _TotalRegistros = entityService.getTotalRegistros();

            var _TotalPaginas = (int)Math.Ceiling((double)_TotalRegistros / _RegistrosPorPagina);
            _Paginador = new PaginadorGenerico<AnalisisSolicitudesInternacion>()
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
        public ActionResult EditItem(string id, string p2, string p3)
        {
            entity = entityService.find(id);
            
            if (entity == null) return RedirectToAction("Index");

            AnalisisSolicitudesInternacionItems item = null;

            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas)  // Extraemos el items
            {
                if (c.ID.items.Equals(p3))
                {
                    item = c;
                    break;
                }
            }

            if (item == null) return RedirectToAction("Index");

            if (item != null && item.resultadoitem.Count == 0)
            {
                // Agregamos los items para el resultado    
                string codigo = p2;   // Verificamos equivalencia                

                NbuinosInternacion nbuinos = entityService.getEquivalenciaPlantilla(p2);
                if (nbuinos != null) codigo = nbuinos.codigo;
                                
                IList<PlantaAnalisisInternacion> items = templateService.getItems(codigo);

                foreach (PlantaAnalisisInternacion it in items)
                {
                    // Plantilla asociada                                
                    PlantaAnalisisInternacionPK PKP = new PlantaAnalisisInternacionPK();
                    PKP.codigo = codigo;
                    PKP.items = it.ID.items;
                    PlantaAnalisisInternacion plantilla = templateService.find(PKP);

                    AnalisisSolicitudesInternacionItemsResultadoPK rpk = new AnalisisSolicitudesInternacionItemsResultadoPK();
                    rpk.codigo = p2;
                    rpk.nrosolicitud = id;
                    rpk.items = it.ID.items;
                    rpk.nroanalisis = item.ID.items;

                    AnalisisSolicitudesInternacionItemsResultado r = new AnalisisSolicitudesInternacionItemsResultado();
                    r.ID = rpk;

                    r.codigo = it.codigo;
                    r.solicituditem = item;
                    r.resultado = it.resultado;
                    r.valoresn = it.valoresn;

                    if (r.resultado == null) r.resultado = " ";
                    if (r.resultado.Equals("")) r.resultado = " ";

                    r.plantilla = plantilla;

                    item.resultadoitem.Add(r);
                }

                entityService.update(entity);
            }

            return View("Result", item);
        }

        [AllowAnonymous]
        public ActionResult ResetItem(string id, string p2)
        {
            entity = entityService.find(id);

            if (entity == null) return RedirectToAction("Index");

            AnalisisSolicitudesInternacionItems item = null;

            string codigo = "";

            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas)  // Extraemos el items
            {
                if (c.ID.items.Equals(p2))
                {
                    item = c;

                    codigo = c.codigo;

                    c.resultadoitem.Clear();
                    c.cargado = null;

                    entityService.update(entity);

                    entity = entityService.find(id);

                    break;
                }
            }

            return EditItem(id, codigo, p2);

        }

        [HttpPost]
        public ActionResult Save(AnalisisSolicitudesInternacionItems list)
        {

            bool resultyes = true;

            string nrosol = list.ID.nrosolicitud;

            string version = utiles.version();

            entity = entityService.find(nrosol);
            entity.version = version;

            string codigo = list.codigo;
            string items = "";

            foreach (AnalisisSolicitudesInternacionItems item in entity.practicas)
            {
                if (item.codigo.Equals(codigo))
                {
                    if (item.cargado == null) resultyes = false;
                    foreach (AnalisisSolicitudesInternacionItemsResultado res in item.resultadoitem)
                    {
                        foreach (AnalisisSolicitudesInternacionItemsResultado rrs in list.resultadoitem)
                        {
                            if (res.ID.items.Equals(rrs.ID.items))
                            {
                                res.resultado = rrs.resultado;
                                res.solicituditem.cargado = "*";
                                items = item.ID.items;
                                break;
                            }
                        }
                    }
                }
            }

            entityService.update(entity);            

            if (!resultyes) return EditItem(entity.nrosolicitud, codigo, items);

            return RedirectToAction("loadProtocol/" + entity.nrosolicitud);
        }


        [AllowAnonymous]
        public ActionResult ObservacionResultItem(string id, string p2, string p3)
        {
            entity = entityService.find(id);

            if (entity == null) return RedirectToAction("Index");

            AnalisisSolicitudesInternacionItemsResultado linea = null;

            foreach (AnalisisSolicitudesInternacionItems it in entity.practicas)
            {
                foreach (AnalisisSolicitudesInternacionItemsResultado re in it.resultadoitem)
                {
                    if (re.ID.codigo.Equals(p2) && re.ID.items.Equals(p3))
                    {
                        linea = re;

                        if (re.observaciones == null)  // Observacion predeterminada
                        {
                            PlantaAnalisisRefInternacionPK PK = new PlantaAnalisisRefInternacionPK();
                            PK.codigo = p2;
                            PK.items = p3;

                            PlantaAnalisisRefInternacion oss = templateRefService.find(PK);

                            if (oss != null) linea.observaciones = oss.observaciones;

                        }

                        if (linea.observaciones == null) linea.observaciones = " ";

                    }
                }
            }


            return View("ResultItemObservacion", linea);

        }


        [HttpPost]
        public ActionResult SaveObservacionResultItem(AnalisisSolicitudesInternacionItemsResultado pojo)
        {

            entity = entityService.find(pojo.solicituditem.solicitud.nrosolicitud);


            string p2 = pojo.ID.codigo;
            string p3 = pojo.ID.items;
            string os = pojo.observaciones;

            AnalisisSolicitudesInternacionItems linea = null;

            foreach (AnalisisSolicitudesInternacionItems it in entity.practicas)
            {
                foreach (AnalisisSolicitudesInternacionItemsResultado re in it.resultadoitem)
                {
                    if (re.ID.codigo.Equals(p2) && re.ID.items.Equals(p3))
                    {
                        re.observaciones = pojo.observaciones;
                        linea = it;
                        break;
                    }
                }
            }

            entityService.update(entity);

            return RedirectToAction("EditItem/" + entity.nrosolicitud + "/" + p2 + "/" + linea.ID.items);

        }

        [AllowAnonymous]
        public ActionResult loadProtocol(string id)
        {
            entity = entityService.find(id);
	    if (entity == null) entity = entityService.find(id.PadLeft(10, '0'));

            if (entity == null)
            {
                Session["error"] = "Protocolo Inexistente " + id;
                return RedirectToAction("Index");
            }

            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;

            return View("Protocol", entity);
        }

        [AllowAnonymous]
        public ActionResult searchPacientes(string id)
        {
            ViewData["protocolospaciente"] = null;

            if (id.ToString().Equals(""))
            {
                ViewData["pacientesresult"] = null;
                return PartialView("Pacientes");
            }
            ViewData["pacientesresult"] = pacienteService.getList(id, 0, 0);
            return PartialView("Pacientes");
        }

        [AllowAnonymous]
        public ActionResult searchProtocolo(string id)
        {
            ViewData["pacientesresult"] = null;

            PacientesInternacion entity = pacienteService.find(id);

            string[] l = { "fecha", "nrosolicitud" };
            ViewData["protocolospaciente"] = entityService.getListProtocolosPaciente(entity, l);

            return PartialView("ProtocolosPaciente");
        }

        [AllowAnonymous]
        public ActionResult searchProtocolosFechas(string id, string p2)
        {

            string[] l = { "fecha", "nrosolicitud" };
            ViewData["protocolospaciente"] = entityService.getListProtocolosFecha(id.Replace("-", ""), p2.Replace("-", ""), l);

            return PartialView("ProtocolosPaciente");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult PrintResult(AnalisisSolicitudesInternacion pojo)
        {
            Session["__idprotocol"] = pojo.nrosolicitud;

            AnalisisSolicitudesInternacion entity = entityService.find(pojo.nrosolicitud);

            var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, pojo.practicas);

            Session["__entitiesreport"] = list;

            return View("PreviewResult");
        }

        [AllowAnonymous]        
        public ActionResult PrintResult2(string id)
        {   

            AnalisisSolicitudesInternacion entity = entityService.find(id);

            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;

            var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);

            Session["__entitiesreport"] = list;

            return View("PreviewResult");
        }

        [AllowAnonymous]
        public ActionResult returnProtocol()
        {
            string id = Session["__idprotocol"].ToString();

            return RedirectToAction("loadProtocol/" + id.ToString());
        }

        [AllowAnonymous]
        public ActionResult IndexPag()
        {
            int pag = 1;

            if (Session["pagina"] != null) pag = Convert.ToInt32(Session["pagina"].ToString());

            return Redirect("Index?pagina=" + pag.ToString());
        }

        [AllowAnonymous]
        public ActionResult SendMail(string id)
        {
            AnalisisSolicitudesInternacion entity = entityService.find(id);
            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;

            entity.emailpara = entity.paciente.email;

            if (entity.emailpara == null) entity.emailpara = "";  // Prorrateamos en Hitórico
            if (entity.emailpara.Equals(""))
            {
                PacientesInternacionMails mail = pacienteService.findPacienteMail(entity.paciente);
                if (mail != null) entity.emailpara = mail.email;
            }

            // email del paciente
            if (entity.paciente.email != null)
                if (!entity.paciente.email.Equals("")) entity.emailpara = entity.paciente.email;

            entity.emailtitulo = "Resultado Protocolo " + entity.nrosolicitud + " - Fecha: " + entity.fecha1 + " - Paciente: " + entity.paciente.nombre;

            StringBuilder linea = new StringBuilder();

            linea.Append("En el presente correo se adjunta archivo con Resultados");

            entity.emailbody = linea.ToString();

            var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);

            Session["__entitiesreport"] = list;
            Session["__idprotocol"] = id;

            // Verificamos si hay archivos Adjuntos
            entity.archivosAdjuntos = attachFileService.getAllProtocolo(id);

            entity.logs = entityService.getListSendResult(id);

            return View("SendMail", entity);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SendMailTo(AnalisisSolicitudesInternacion pojo)
        {
            string estado = "";

            if (pojo.emailpara == null) return RedirectToAction("/SendMail/" + pojo.nrosolicitud);

            if (!utiles.validarEmail(pojo.emailpara))
            {
                estado = "ERROR: dirección de email incorrecta";

                Session["error"] = estado;

                return Redirect("IndexPag");
            }

            AnalisisSolicitudesInternacion entity = entityService.find(pojo.nrosolicitud);

            // Archivos adjuntos
            entity.archivosAdjuntos = attachFileService.getAllProtocolo(pojo.nrosolicitud);

            Parametros parametro = parametroService.find(1);

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(parametro.Parametro9);
            correo.To.Add(new MailAddress(pojo.emailpara));

            correo.Subject = pojo.emailtitulo;
            correo.Body = pojo.emailbody;
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = parametro.Parametro7;
            smtp.Credentials = new System.Net.NetworkCredential(parametro.Parametro9, parametro.Parametro10);
            smtp.Port = Convert.ToInt32(parametro.Parametro8);
            smtp.EnableSsl = false;

            //Adjuntamos archivo de resultado
            Attachment at = new Attachment(Server.MapPath("~/work/pdf/" + pojo.nrosolicitud + ".pdf"));
            correo.Attachments.Add(at);

            // Archivos adjuntos
            foreach (AttachFilesInternacion c in entity.archivosAdjuntos)
            {
                at = new Attachment(Server.MapPath(c.Archivo));
                correo.Attachments.Add(at);
            }

            try
            {
                smtp.Send(correo);
                estado = "Mensaje Enviado Correctamente";
                correo.Attachments.Dispose();
                smtp.Dispose();

                // LOG
                AnalisisSolicitudesInternacionSendResults log = new AnalisisSolicitudesInternacionSendResults();
                log.id = utiles.guiid();
                log.fechahora = DateTime.Now;
                log.solicitud = entity;
                entityService.saveSendMail(log);

                // Email de Referencia
                PacientesInternacionMails c = new PacientesInternacionMails();
                c.email = pojo.emailpara;
                c.paciente = entity.paciente;
                pacienteService.saveMail(c);

                // Eliminamos el archivo
                if (System.IO.File.Exists(Server.MapPath("~/work/pdf/" + pojo.nrosolicitud + ".pdf")))
                    System.IO.File.Delete(Server.MapPath("~/work/pdf/" + pojo.nrosolicitud + ".pdf"));
            }
            catch (Exception ex)
            {
                estado = "ERROR: " + ex.Message;
            }

            Session["error"] = estado;

            return Redirect("IndexPag");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            // Borramos archivos previos
            attachFileService.removeFileProtocol(Session["__idprotocol"].ToString());

            int i = 0;
            int maxSize = 1048576;

            foreach (HttpPostedFileBase file in files)
            {
                if (file == null) return RedirectToAction("/SendMail/" + Session["__idprotocol"].ToString());

                if (file.ContentLength <= maxSize)
                {
                    Guid obj = Guid.NewGuid();
                    string path = Path.Combine(Server.MapPath("~/work/attach_int"), Path.GetFileName(obj.ToString() + file.FileName));
                    file.SaveAs(path);
                    string filepath = "/work/attach_int/" + obj.ToString() + file.FileName;

                    AttachFilesInternacion entity = new AttachFilesInternacion();
                    entity.Id = obj.ToString();
                    entity.Archivo = filepath;
                    entity.Fecha = DateTime.Now;
                    entity.Protocolo = Session["__idprotocol"].ToString();
                    entity.Original = file.FileName;

                    attachFileService.persist(entity);

                    i++;
                }
            }

            return RedirectToAction("/SendMail/" + Session["__idprotocol"].ToString());

        }

        [AllowAnonymous]
        public ActionResult SendMailDeleteFile(string id, string p2)
        {
            var l = attachFileService.getAllProtocolo(id);

            foreach (AttachFilesInternacion c in l)
            {
                if (c.Id.Equals(p2))
                {
                    attachFileService.remove(c);
                    break;
                }
            }

            return RedirectToAction("/SendMail/" + id);
        }

        [AllowAnonymous]
        public ActionResult Senddcm4che(string id)
        {
            AnalisisSolicitudesInternacion entity = entityService.find(id);
            foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;

            entity.codpac = (entity.paciente != null && entity.paciente.codpac != null) ? entity.paciente.codpac : "";

            var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);

            Session["__entitiesreport"] = list;
            Session["__idprotocol"] = id;

            entity.logsDcm4che = entityService.getListSenddcm4che(id);

            if (Session["error"] != null) ViewBag.error = Session["error"];
            Session["error"] = null;

            return View("Senddcm4che", entity);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Senddcm4cheTo(AnalisisSolicitudesInternacion pojo)
        {
            string estado = "";

            AnalisisSolicitudesInternacion entity = entityService.find(pojo.nrosolicitud);

            string externalId = pojo.nrosolicitud;
            string patientDni = "";
            if (pojo.paciente != null && !string.IsNullOrEmpty(pojo.paciente.nrodoc))
                patientDni = pojo.paciente.nrodoc.Trim();
            else if (entity.paciente != null && !string.IsNullOrEmpty(entity.paciente.nrodoc))
                patientDni = entity.paciente.nrodoc.Trim();

            if (string.IsNullOrEmpty(patientDni))
            {
                Session["error"] = "ERROR: número de documento requerido";
                return RedirectToAction("/Senddcm4che/" + pojo.nrosolicitud);
            }
            string patientName = (entity.paciente != null && entity.paciente.nombre != null) ? entity.paciente.nombre : "";
            string studyDate = FormatStudyDate(entity.fecha);
            string studyType = "LAB";

            try
            {
                foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;
                var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);
                Session["__entitiesreport"] = list;

                string pdfPath = Server.MapPath("/work/pdf/" + pojo.nrosolicitud + ".pdf");
                byte[] pdfBytes = PrintReportController.RenderAndSavePdf(Server, list, "ReportProtocoloResult", pojo.nrosolicitud);

                Parametros parametro = parametroService.find(1);
                if (parametro == null || string.IsNullOrEmpty(parametro.Titulo1) || string.IsNullOrEmpty(parametro.Titulo3))
                {
                    Session["error"] = "ERROR: parámetros de dcm4che incompletos";
                    return RedirectToAction("/Senddcm4che/" + pojo.nrosolicitud);
                }

                string url = parametro.Titulo1;
                string apiKey = parametro.Titulo3;

                HttpStatusCode statusCode;
                string detalle;
                PostDcm4cheMultipart(url, apiKey, externalId, patientDni, patientName, studyDate, studyType, pdfBytes, Path.GetFileName(pdfPath), out statusCode, out detalle);

                if (string.IsNullOrEmpty(detalle)) detalle = statusCode.ToString();
                if (detalle.Length > 255) detalle = detalle.Substring(0, 255);

                if (statusCode >= HttpStatusCode.OK && statusCode < HttpStatusCode.Ambiguous)
                    estado = "Enviado Correctamente a dcm4che";
                else if (statusCode == HttpStatusCode.NotFound)
                    estado = "ERROR: no se encontró el destino o el paciente en dcm4che";
                else
                    estado = "ERROR: " + statusCode + " " + detalle;

                if (estado.Length > 255) estado = estado.Substring(0, 255);

                AnalisisSolicitudesInternacionSenddcm4che log = new AnalisisSolicitudesInternacionSenddcm4che();
                log.id = utiles.guiid();
                log.fechahora = DateTime.Now;
                log.solicitud = entity;
                log.opt1 = patientDni;
                log.opt2 = statusCode.ToString();
                log.opt3 = detalle;
                log.estado = estado;
                entityService.saveSenddcm4che(log);
            }
            catch (Exception ex)
            {
                estado = "ERROR: " + ex.Message;

                string detalle = ex.Message;
                if (detalle != null && detalle.Length > 255) detalle = detalle.Substring(0, 255);

                AnalisisSolicitudesInternacionSenddcm4che log = new AnalisisSolicitudesInternacionSenddcm4che();
                log.id = utiles.guiid();
                log.fechahora = DateTime.Now;
                log.solicitud = entity;
                log.opt1 = patientDni;
                log.opt2 = "ERROR";
                log.opt3 = detalle;
                log.estado = estado.Length > 255 ? estado.Substring(0, 255) : estado;
                entityService.saveSenddcm4che(log);
            }

            Session["error"] = estado;

            return Redirect("IndexPag");
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
