using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Http;
using System.Web;
using System;
using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.Controllers
{
    public class RestSincroInternacionController : ApiController
    {
        IPacientesInternacionService pacienteService = (IPacientesInternacionService)SpringContext.Instance.GetObject("PacientesInternacionService");
        INomenclaturaInternacionService nomenclaturaService = (INomenclaturaInternacionService)SpringContext.Instance.GetObject("NomenclaturaInternacionService");
        IObrasSocialesInternacionService obrasocialService = (IObrasSocialesInternacionService)SpringContext.Instance.GetObject("ObrasSocialesInternacionService");
        IPlantaAnalisisInternacionService plantillaService = (IPlantaAnalisisInternacionService)SpringContext.Instance.GetObject("PlantaAnalisisInternacionService");
        IProfesionalesInternacionService profesionalService = (IProfesionalesInternacionService)SpringContext.Instance.GetObject("ProfesionalesInternacionService");
        IPlantaAnalisisRefInternacionService plantillarefService = (IPlantaAnalisisRefInternacionService)SpringContext.Instance.GetObject("PlantaAnalisisRefInternacionService");
        INbuInternacionService nbuService = (INbuInternacionService)SpringContext.Instance.GetObject("NbuInternacionService");
        IAnalisisSolicitudesInternacionService solicitudService = (IAnalisisSolicitudesInternacionService)SpringContext.Instance.GetObject("AnalisisSolicitudesInternacionService");
        

        string res = "";
        StringBuilder tipo = new StringBuilder();

        string ruta = "~/work/import/"; // Ruta por defecto       

        private string moveFiles()
        {
            // cuando el FTP copia un arhivo en otro sitio

            res = "";
            if (File.Exists(HttpContext.Current.Server.MapPath("~/config/archsincroint.txt")))
            {

                string filePath = HttpContext.Current.Server.MapPath("~/config/archsincroint.txt");

                string[] dir = File.ReadAllLines(filePath);

                string[] jsonFiles = Directory.GetFiles(dir[0], "*.json");

                try
                {

                    foreach (string jsonFile in jsonFiles)
                    {
                        var js = Path.GetFileName(jsonFile);

                        File.Copy(dir[0] + "/" + js, dir[1] + "/" + js, true);
                    }

                    res = "OK";
                }
                catch (Exception e)
                {
                    res = e.Message;
                }
            }

            return res;
        }

        [HttpGet, ActionName("all")]
        public string all(int id)
        {
            if (id == 3 || id == 0) moveFiles();

            nomenclatura(0);
            nbu(0);
            pacientes(0);
            obrassociales(0);
            profesionales(0);
            plantaanalisis(0);
            plantaanalisisref(0);
            nbuinos(0);
            analisissolicitudes(0);

            return "OK";
        }

        [HttpGet, ActionName("nomenclatura")]
        public string nomenclatura(int id)
        {
            res = "Actualizando Nomenclaturas";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nomenclatura_internacion.json")))
            {
                tipo.Clear();
                List<NomenclaturaInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nomenclatura_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<NomenclaturaInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<NomenclaturaInternacion>));
                }

                nomenclaturaService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("pacientes")]
        public string pacientes(int id)
        {
            res = "Actualizando Pacientes";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/pacientes_internacion.json")))
            {
                tipo.Clear();
                List<PacientesInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/pacientes_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<PacientesInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<PacientesInternacion>));
                }

                pacienteService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }        

        [HttpGet, ActionName("obrassociales")]
        public string obrassociales(int id)
        {
            res = "Actualizando Obras Sociales";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/obrassociales_internacion.json")))
            {
                tipo.Clear();
                List<ObrasSocialesInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/obrassociales_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<ObrasSocialesInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<ObrasSocialesInternacion>));
                }

                obrasocialService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("plantaanalisis")]
        public string plantaanalisis(int id)
        {
            res = "Actualizando Plantilla de Análisis";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/plantanalisis_internacion.json")))
            {
                tipo.Clear();
                List<PlantaAnalisisInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/plantanalisis_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<PlantaAnalisisInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<PlantaAnalisisInternacion>));
                }

                foreach (PlantaAnalisisInternacion c in entities)
                {
                    PlantaAnalisisInternacion entity = new PlantaAnalisisInternacion();
                    entity.ID = new PlantaAnalisisInternacionPK();

                    entity.ID.codigo = c.codigo;
                    entity.ID.items = c.items;
                    entity.elemento = c.elemento;
                    entity.valoresn = c.valoresn;
                    entity.resultado = c.resultado;
                    entity.imputable = c.imputable;
                    entity.distancia = c.distancia;
                    entity.itemsparalelo = c.itemsparalelo;
                    entity.formula = c.formula;
                    entity.observaciones = c.observaciones;

                    plantillaService.persist(entity);

                }

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("profesionales")]
        public string profesionales(int id)
        {
            res = "Actualizando Profesionales";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales_internacion.json")))
            {
                tipo.Clear();
                List<ProfesionalesInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<ProfesionalesInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<ProfesionalesInternacion>));
                }

                profesionalService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("plantaanalisisref")]
        public string plantaanalisisref(int id)
        {
            res = "Actualizando Referencias Plantilla de Análisis";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refplantanalisis_internacion.json")))
            {
                tipo.Clear();
                List<PlantaAnalisisRefInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refplantanalisis_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<PlantaAnalisisRefInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<PlantaAnalisisRefInternacion>));
                }

                foreach (PlantaAnalisisRefInternacion c in entities)
                {
                    PlantaAnalisisRefInternacion entity = new PlantaAnalisisRefInternacion();

                    PlantaAnalisisRefInternacionPK PK = new PlantaAnalisisRefInternacionPK();
                    PK.codigo = c.codigo;
                    PK.items = c.items;

                    entity.ID = PK;
                    entity.observaciones = c.observacion;

                    plantillarefService.persist(entity);
                }

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("nbu")]
        public string nbu(int id)
        {
            res = "Actualizando Nbu";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nbu_internacion.json")))
            {
                tipo.Clear();
                List<NbuInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nbu_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<NbuInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<NbuInternacion>));
                }

                nbuService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("reset")]
        public string reset(int id)
        {
            
            var solicitudes = solicitudService.getAll(null, null, null);
            foreach (AnalisisSolicitudesInternacion c in solicitudes) solicitudService.remove(c);

            var plantillasref = plantillarefService.getAll(0, 0);
            foreach (PlantaAnalisisRefInternacion c in plantillasref) plantillarefService.remove(c);

            var plantillas = plantillaService.getAll(0, 0);
            foreach (PlantaAnalisisInternacion c in plantillas) plantillaService.remove(c);

            var pacientes = pacienteService.getAll(0, 0);
            foreach (PacientesInternacion c in pacientes) pacienteService.remove(c);

            solicitudes.Clear();
            plantillas.Clear();
            plantillasref.Clear();
           

            return "OK";
        }

        [HttpGet, ActionName("nbuinos")]
        public string nbuinos(int id)
        {
            res = "Actualizando Nbu INOS";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refinos_internacion.json")))
            {
                tipo.Clear();
                List<NbuinosInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refinos_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<NbuinosInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<NbuinosInternacion>));
                }

                solicitudService.saveNbuinos(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("analisissolicitudes")]
        public string analisissolicitudes(int id)
        {
            res = "Actualizando Solicitudes Analisis";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/protocolos_internacion.json")))
            {

                tipo.Clear();
                List<AnalisisSolicitudesInternacion> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/protocolos_internacion.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<AnalisisSolicitudesInternacion>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<AnalisisSolicitudesInternacion>));
                }

                foreach (AnalisisSolicitudesInternacion c in entities)
                {
                    AnalisisSolicitudesInternacion entity = new AnalisisSolicitudesInternacion();
                    entity.nrosolicitud = c.protocolo;
                    entity.protocolo = c.protocolo;
                    entity.obrasocial = obrasocialService.find(c.codos);
                    entity.paciente = pacienteService.find(c.codpac);
                    entity.profesional = profesionalService.find(c.idprof);
                    entity.fecha = c.fecha;

                    // Practicas
                    foreach (AnalisisSolicitudesInternacionItems item in c.practicas)
                    {
                        AnalisisSolicitudesInternacionItems linea = new AnalisisSolicitudesInternacionItems();

                        linea.ID = new AnalisisSolicitudesInternacionItemsPK();
                        linea.ID.nrosolicitud = c.protocolo;
                        linea.ID.items = item.items;

                        linea.practica = nomenclaturaService.find(item.codigo);
                        linea.nbu = nbuService.find(item.codigo);
                        linea.items = item.items;
                        linea.solicitud = entity;
                        linea.codigo = item.codigo;

                        // Plantilla asociada
                        string codigo = item.codigo;   // Verificamos equivalencia                        
                        NbuinosInternacion nbuinos = solicitudService.getEquivalenciaPlantilla(item.codigo);
                        if (nbuinos != null) codigo = nbuinos.codigo;

                        // Resultados
                        if (item.resultadoitem != null)
                        {
                            foreach (AnalisisSolicitudesInternacionItemsResultado result in item.resultadoitem)
                            {
                                AnalisisSolicitudesInternacionItemsResultado lineares = new AnalisisSolicitudesInternacionItemsResultado();

                                lineares.ID = new AnalisisSolicitudesInternacionItemsResultadoPK();
                                lineares.ID.nrosolicitud = c.protocolo;
                                lineares.ID.items = result.items;
                                lineares.ID.codigo = result.codigo;
                                lineares.ID.nroanalisis = result.nroanalisis;

                                lineares.resultado = result.resultado;
                                lineares.valoresn = result.valoresn;
                                lineares.solicituditem = linea;

                                PlantaAnalisisInternacionPK PKP = new PlantaAnalisisInternacionPK();
                                PKP.codigo = codigo;
                                PKP.items = result.items;  //System.Diagnostics.Debug.WriteLine("pasa --- " + c.protocolo + " " + codigo + " " + item.codigo + " ----- " + PKP.codigo + "-" + PKP.items);

                                lineares.plantilla = plantillaService.find(PKP);

                                lineares.observaciones = result.observaciones;

                                linea.resultadoitem.Add(lineares);
                            }
                        }                        

                        entity.practicas.Add(linea);

                    }

                    solicitudService.updateBatch(entity);

                }

                entities.Clear();

            }

            return res;
        }

    }
}
