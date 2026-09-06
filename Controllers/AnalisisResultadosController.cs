using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico._filters;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.utiles;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.Controllers
{
    [AuthorizationFilter]
    public class AnalisisResultadosController : Controller
    {
        IAnalisisSolicitudesService entityService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");
        IPlantaAnalisisService templateService = (IPlantaAnalisisService)SpringContext.Instance.GetObject("PlantaAnalisisService");
        IPlantaAnalisisRefService templateRefService = (IPlantaAnalisisRefService)SpringContext.Instance.GetObject("PlantaAnalisisRefService");
        IPacientesService pacienteService = (IPacientesService)SpringContext.Instance.GetObject("PacientesService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");
        IParametrosService parametroService = (IParametrosService)SpringContext.Instance.GetObject("ParametrosService");
        IAttachFilesAmbulatorioService attachFileService = (IAttachFilesAmbulatorioService)SpringContext.Instance.GetObject("AttachFilesAmbulatorioService");

        Utiles utiles = new Utiles();
        AnalisisSolicitudes entity = null;

        private readonly int _RegistrosPorPagina = 8;
        private PaginadorGenerico<AnalisisSolicitudes> _Paginador;
        private IList<AnalisisSolicitudes> entities;

        ModelEstadistica filtro = null;

        string[] l = { "fecha", "nrosolicitud" };

        [AllowAnonymous]
        public ActionResult Index(int pagina = 1)
        {

            entities = entityService.getAll(pagina, _RegistrosPorPagina, l);

            long _TotalRegistros = entityService.getTotalRegistros();

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
        public ActionResult EditItem(string id, string p2, string p3)
        {
            entity = entityService.find(id);

            if (entity == null) return RedirectToAction("Index");

            AnalisisSolicitudesItems item = null;

            foreach (AnalisisSolicitudesItems c in entity.practicas)  // Extraemos el items
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

                Nbuinos nbuinos = entityService.getEquivalenciaPlantilla(p2);
                if (nbuinos != null) codigo = nbuinos.codigo;

                //System.Diagnostics.Debug.WriteLine("pasa 1 " + codigo);
                IList<PlantaAnalisis> items = templateService.getItems(codigo);

                foreach (PlantaAnalisis it in items)
                {
                    // Plantilla asociada                                
                    PlantaAnalisisPK PKP = new PlantaAnalisisPK();
                    PKP.codigo = codigo;
                    PKP.items = it.ID.items;
                    PlantaAnalisis plantilla = templateService.find(PKP);

                    AnalisisSolicitudesItemsResultadoPK rpk = new AnalisisSolicitudesItemsResultadoPK();
                    rpk.codigo = p2;
                    rpk.nrosolicitud = id;
                    rpk.items = it.ID.items;
                    rpk.nroanalisis = item.ID.items;

                    AnalisisSolicitudesItemsResultado r = new AnalisisSolicitudesItemsResultado();
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

            AnalisisSolicitudesItems item = null;

            string codigo = "";

            foreach (AnalisisSolicitudesItems c in entity.practicas)  // Extraemos el items
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
        public ActionResult Save(AnalisisSolicitudesItems list)
        {

            bool resultyes = true;

            string nrosol = list.ID.nrosolicitud;

            string version = utiles.version();

            entity = entityService.find(nrosol);
            entity.version = version;

            string codigo = list.codigo;
            string items = "";

            foreach (AnalisisSolicitudesItems item in entity.practicas)
            {
                if (item.codigo.Equals(codigo))
                {
                    if (item.cargado == null) resultyes = false;
                    foreach (AnalisisSolicitudesItemsResultado res in item.resultadoitem)
                    {
                        foreach (AnalisisSolicitudesItemsResultado rrs in list.resultadoitem)
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

            //System.Diagnostics.Debug.WriteLine("xxx  " + items);            

            if (!resultyes) return EditItem(entity.nrosolicitud, codigo, items);

            return RedirectToAction("loadProtocol/" + entity.nrosolicitud);
        }


        [AllowAnonymous]
        public ActionResult ObservacionResultItem(string id, string p2, string p3)
        {
            entity = entityService.find(id);

            if (entity == null) return RedirectToAction("Index");

            AnalisisSolicitudesItemsResultado linea = null;

            foreach (AnalisisSolicitudesItems it in entity.practicas)
            {
                foreach (AnalisisSolicitudesItemsResultado re in it.resultadoitem)
                {
                    if (re.ID.codigo.Equals(p2) && re.ID.items.Equals(p3))
                    {
                        linea = re;

                        if (re.observaciones == null)  // Observacion predeterminada
                        {
                            PlantaAnalisisRefPK PK = new PlantaAnalisisRefPK();
                            PK.codigo = p2;
                            PK.items = p3;

                            PlantaAnalisisRef oss = templateRefService.find(PK);

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

            AnalisisSolicitudesItems linea = null;

            foreach (AnalisisSolicitudesItems it in entity.practicas)
            {
                foreach (AnalisisSolicitudesItemsResultado re in it.resultadoitem)
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

            if (entity == null)
            {
                Session["error"] = "Protocolo Inexistente " + id;
                return RedirectToAction("Index");
            }

            // Verificamos si hay archivos Adjuntos
            entity.archivosAdjuntos = attachFileService.getAllProtocolo(id);

            foreach (AnalisisSolicitudesItems c in entity.practicas) c.Select = true;

            Session["__idprotocol"] = id;

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

            Pacientes entity = pacienteService.find(id);

            string[] l = { "fecha", "nrosolicitud" };
            ViewData["protocolospaciente"] = entityService.getListProtocolosPaciente(entity, l);

            return PartialView("ProtocolosPaciente");
        }

        [AllowAnonymous]
        public ActionResult searchProtocolosFechas(string id, string p2)
        {
            filtro = new ModelEstadistica();
            filtro.fecha1 = id;
            filtro.fecha2 = p2;
            Session["__filtro"] = filtro;

            string[] l = { "fecha", "nrosolicitud" };
            ViewData["protocolospaciente"] = entityService.getListProtocolosFecha(id.Replace("-", "") , p2.Replace("-", ""), l);

            return PartialView("ProtocolosPaciente");
        }

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult PrintResult(AnalisisSolicitudes pojo)
        {
            Session["__idprotocol"] = pojo.nrosolicitud;

            AnalisisSolicitudes entity = entityService.find(pojo.nrosolicitud);

            var list = reportService.getProtocoloResult(entity.nrosolicitud, pojo.practicas);

            Session["__entitiesreport"] = list;

            return View("PreviewResult");
        }

        [AllowAnonymous]        
        public ActionResult PrintResult2(string id)
        {
            AnalisisSolicitudes entity = entityService.find(id);

            foreach (AnalisisSolicitudesItems c in entity.practicas) c.Select = true;

            var list = reportService.getProtocoloResult(entity.nrosolicitud, entity.practicas);

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

            AnalisisSolicitudes entity = entityService.find(id);
            foreach (AnalisisSolicitudesItems c in entity.practicas) c.Select = true;

            entity.emailpara = entity.paciente.email;

            if (entity.emailpara == null) entity.emailpara = "";  // Prorrateamos en Hitórico
            if (entity.emailpara.Equals(""))
            {
                PacientesMails mail = pacienteService.findPacienteMail(entity.paciente);
                if (mail != null) entity.emailpara = mail.email;
            }

            // email del paciente
            if (entity.paciente.email != null)
                if (!entity.paciente.email.Equals("")) entity.emailpara = entity.paciente.email;

            entity.emailtitulo = "Resultado Protocolo " + entity.nrosolicitud + " - Fecha: " + entity.fecha1 + " - Paciente: " + entity.paciente.nombre;

            StringBuilder linea = new StringBuilder();

            linea.Append("En el presente correo se adjunta archivo con Resultados");

            entity.emailbody = linea.ToString();

            var list = reportService.getProtocoloResult(entity.nrosolicitud, entity.practicas);

            Session["__entitiesreport"] = list;
            Session["__idprotocol"] = id;

            // Verificamos si hay archivos Adjuntos
            entity.archivosAdjuntos = attachFileService.getAllProtocolo(id);

            entity.logs = entityService.getListSendResult(id);

            return View("SendMail", entity);
        }

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult SendMailTo(AnalisisSolicitudes pojo)
        {
            string estado = "";

            if (pojo.emailpara == null) return RedirectToAction("/SendMail/" + pojo.nrosolicitud);

            if (!utiles.validarEmail(pojo.emailpara))
            {
                estado = "ERROR: dirección de email incorrecta";

                Session["error"] = estado;

                return RedirectToAction("/SendMail/" + pojo.nrosolicitud);
            }

            AnalisisSolicitudes entity = entityService.find(pojo.nrosolicitud);

            // Archivos adjuntos
            entity.archivosAdjuntos = attachFileService.getAllProtocolo(pojo.nrosolicitud);

            Parametros parametro = parametroService.find(1);

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress(parametro.Parametro9);
            correo.To.Add(new MailAddress(pojo.emailpara.ToLower()));

            correo.Subject = pojo.emailtitulo;
            correo.Body = pojo.emailbody;
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = parametro.Parametro7;
            smtp.Credentials = new System.Net.NetworkCredential(parametro.Parametro9, parametro.Parametro10);
            smtp.Port = Convert.ToInt32(parametro.Parametro8);
            smtp.EnableSsl = false;
            
            Attachment at = new Attachment(Server.MapPath("~/work/pdf/" + pojo.nrosolicitud + ".pdf"));
            correo.Attachments.Add(at);

            // Archivos adjuntos
            foreach(AttachFilesAmbulatorio c in entity.archivosAdjuntos)
            {
                Attachment att = new Attachment(Server.MapPath(c.Archivo));
                correo.Attachments.Add(att);
            }

            try
            {
                smtp.Send(correo);
                estado = "Mensaje Enviado Correctamente";
                correo.Attachments.Dispose();
                smtp.Dispose();

                // LOG
                AnalisisSolicitudesSendResults log = new AnalisisSolicitudesSendResults();
                log.id = utiles.guiid();
                log.fechahora = DateTime.Now;
                log.solicitud = entity;
                entityService.saveSendMail(log);

                // Email de Referencia
                PacientesMails c = new PacientesMails();
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

            smtp.Dispose();
           
            Session["error"] = estado;            

            return Redirect("IndexPag");
        }

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files)
        {
            // Borramos archivos previos
            //attachFileService.removeFileProtocol(Session["__idprotocol"].ToString());

            int i = 0;
            int maxSize = 1048576;

            foreach (HttpPostedFileBase file in files)
            {
                if (file == null) return RedirectToAction("/SendMail/" + Session["__idprotocol"].ToString());

                if (file.ContentLength <= maxSize)
                {
                    Guid obj = Guid.NewGuid();
                    string path = Path.Combine(Server.MapPath("~/work/attach_amb"), Path.GetFileName(obj.ToString() + file.FileName));
                    file.SaveAs(path);
                    string filepath = "/work/attach_amb/" + obj.ToString() + file.FileName;

                    AttachFilesAmbulatorio entity = new AttachFilesAmbulatorio();
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

        //[AllowAnonymous]
        [HttpPost]
        public ActionResult UploadFilesProtocol(HttpPostedFileBase[] files)
        {
            // Borramos archivos previos
            //attachFileService.removeFileProtocol(Session["__idprotocol"].ToString());

            int i = 0;
            int maxSize = 1048576;

            foreach (HttpPostedFileBase file in files)
            {
                if (file == null) return RedirectToAction("/SendMail/" + Session["__idprotocol"].ToString());

                if (file.ContentLength <= maxSize)
                {
                    Guid obj = Guid.NewGuid();
                    string path = Path.Combine(Server.MapPath("~/work/attach_amb"), Path.GetFileName(obj.ToString() + file.FileName));
                    file.SaveAs(path);
                    string filepath = "/work/attach_amb/" + obj.ToString() + file.FileName;

                    AttachFilesAmbulatorio entity = new AttachFilesAmbulatorio();
                    entity.Id = obj.ToString();
                    entity.Archivo = filepath;
                    entity.Fecha = DateTime.Now;
                    entity.Protocolo = Session["__idprotocol"].ToString();
                    entity.Original = file.FileName;

                    attachFileService.persist(entity);

                    i++;
                }
            }

            return RedirectToAction("/loadProtocol/" + Session["__idprotocol"].ToString());

        }

        //[AllowAnonymous]
        public ActionResult SendMailDeleteFile(string id, string p2)
        {
            var l = attachFileService.getAllProtocolo(id);

            foreach(AttachFilesAmbulatorio c in l)
            {
                if (c.Id.Equals(p2))
                {
                    attachFileService.remove(c);
                    break;
                }
            }

            return RedirectToAction("/SendMail/" + id);
        }

        //[AllowAnonymous]
        public ActionResult ProtocolDeleteFile(string id, string p2)
        {
            var l = attachFileService.getAllProtocolo(id);

            foreach (AttachFilesAmbulatorio c in l)
            {
                if (c.Id.Equals(p2))
                {
                    attachFileService.remove(c);
                    break;
                }
            }

            return RedirectToAction("/loadProtocol/" + id);
        }

       //[AllowAnonymous]        
        public JsonResult getFechas(string id)
        {
            ModelEstadistica c = (ModelEstadistica)Session["__filtro"];
            return Json(c, JsonRequestBehavior.AllowGet);
        }


    }
}
