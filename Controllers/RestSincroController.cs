using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;

namespace laboratoriobioquimico.Controllers
{
    public class RestSincroController : ApiController
    {
        INomenclaturaService nomenclaturaService = (INomenclaturaService)SpringContext.Instance.GetObject("NomenclaturaService");
        IObrasSocialesService obrasocialService = (IObrasSocialesService)SpringContext.Instance.GetObject("ObrasSocialesService");
        IPlantaAnalisisService plantillaService = (IPlantaAnalisisService)SpringContext.Instance.GetObject("PlantaAnalisisService");
        IPlantaAnalisisRefService plantillaServiceref = (IPlantaAnalisisRefService)SpringContext.Instance.GetObject("PlantaAnalisisRefService");
        IProfesionalesService profesionalService = (IProfesionalesService)SpringContext.Instance.GetObject("ProfesionalesService");
        IPacientesService pacienteService = (IPacientesService)SpringContext.Instance.GetObject("PacientesService");
        IAnalisisSolicitudesService solicitudService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");
        INbuService nbuService = (INbuService)SpringContext.Instance.GetObject("NbuService");
        IProfesionalesUsersService profesionaluserService = (IProfesionalesUsersService)SpringContext.Instance.GetObject("ProfesionalesUsersService");

        string res = "";
        StringBuilder tipo = new StringBuilder();

        string ruta = "~/work/import/"; // Ruta por defecto        

        private string getRuta()
        {
            if (solicitudService.getRutaSincro().Length > 0) ruta = solicitudService.getRutaSincro() + "/";

            // La recuperamos del archivo
            if (File.Exists(HttpContext.Current.Server.MapPath("~/config/archsincro.txt")))
            {
                string filePath = HttpContext.Current.Server.MapPath("~/config/archsincro.txt");
                string[] dir = File.ReadAllLines(filePath);
                ruta = dir[1] + "/";

            }

            return ruta;
        }

        private string moveFiles()
        {
            // cuando el FTP copia un arhivo en otro sitio

            res = "";
            if (File.Exists(HttpContext.Current.Server.MapPath("~/config/archsincro.txt")))
            {

                string filePath = HttpContext.Current.Server.MapPath("~/config/archsincro.txt");

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

        [HttpGet, ActionName("tesfile")]
        public string testfile(string id)
        {
            var r = "No";

            //if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(ruta))) r = "Si";            
            if (File.Exists(getRuta() + "nomenclatura.json")) r = "Si";

            tipo.Clear();
            using (StreamReader file = File.OpenText(getRuta() + "obrassociales.json"))
            {
                tipo.Append(file.ReadLine());
            }

            return r + " " + getRuta() + " " + tipo.ToString();  //+ " " + .System.Web.HttpContext.Current.Server.MapPath("~");
        }

        [HttpGet, ActionName("all")]
        public string all(int id)
        {

            if (id == 3 || id == 0) moveFiles();

            nomenclatura(0);
            obrassociales(0);
            pacientes(0);
            nomenclatura(0);
            plantaanalisis(0);
            plantaanalisisref(0);
            profesionales(0);
            nbu(0);
            nbuinos(0);
            profesionalesusers(0);

            analisissolicitudes(0);

            return "OK pasa";
        }

        [HttpGet, ActionName("reset")]
        public string reset(int id)
        {
            var solicitudes = solicitudService.getAll(null, null, null);
            foreach (AnalisisSolicitudes c in solicitudes) solicitudService.remove(c);

            var plantillasref = plantillaServiceref.getAll(0, 0);
            foreach (PlantaAnalisisRef c in plantillasref) plantillaServiceref.remove(c);

            var plantillas = plantillaService.getAll(0, 0);
            foreach (PlantaAnalisis c in plantillas) plantillaService.remove(c);

            var pacientes = pacienteService.getAll(0, 0);
            foreach (Pacientes c in pacientes) pacienteService.remove(c);


            solicitudes.Clear();
            plantillas.Clear();
            plantillasref.Clear();

            return "OK";
        }

        [HttpGet, ActionName("nomenclatura")]
        public string nomenclatura(int id)
        {
            res = "Actualizando Nomenclaturas";

            if (File.Exists(HttpContext.Current.Server.MapPath("~/work/import/nomenclatura.json")))
            {
                tipo.Clear();
                List<Nomenclatura> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nomenclatura.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<Nomenclatura>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<Nomenclatura>));
                }

                nomenclaturaService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("obrassociales")]
        public string obrassociales(int id)
        {
            res = "Actualizando Obras Sociales";

            if (File.Exists(HttpContext.Current.Server.MapPath("~/work/import/obrassociales.json")))
            //if (File.Exists(getRuta() + "obrassociales.json"))
            {
                tipo.Clear();
                List<ObrasSociales> entities;
                using (StreamReader file = File.OpenText(HttpContext.Current.Server.MapPath("~/work/import/obrassociales.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<ObrasSociales>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<ObrasSociales>));
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

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/plantanalisis.json")))

            {
                tipo.Clear();
                List<PlantaAnalisis> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/plantanalisis.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<PlantaAnalisis>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<PlantaAnalisis>));
                }

                foreach (PlantaAnalisis c in entities)
                {
                    PlantaAnalisis entity = new PlantaAnalisis();
                    entity.ID = new PlantaAnalisisPK();

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

        [HttpGet, ActionName("plantaanalisisref")]
        public string plantaanalisisref(int id)
        {
            res = "Actualizando Referencias Plantilla de Análisis";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refplantanalisis.json")))
                if (File.Exists(getRuta() + "refplantanalisis.json"))
                {
                    tipo.Clear();
                    List<PlantaAnalisisRef> entities;
                    using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refplantanalisis.json")))
                    {
                        tipo.Append(file.ReadLine());
                        entities = (List<PlantaAnalisisRef>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<PlantaAnalisisRef>));
                    }

                    foreach (PlantaAnalisisRef c in entities)
                    {
                        PlantaAnalisisRef entity = new PlantaAnalisisRef();

                        PlantaAnalisisRefPK PK = new PlantaAnalisisRefPK();
                        PK.codigo = c.codigo;
                        PK.items = c.items;

                        entity.ID = PK;
                        entity.observaciones = c.observaciones;

                        plantillaServiceref.persist(entity);
                    }

                    entities.Clear();
                }

            return res;
        }

        [HttpGet, ActionName("profesionales")]
        public string profesionales(int id)
        {
            res = "Actualizando Profesionales";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales.json")))
                if (File.Exists(getRuta() + "profesionales.json"))
                {
                    tipo.Clear();
                    List<Profesionales> entities;
                    using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales.json")))
                    {
                        tipo.Append(file.ReadLine());
                        entities = (List<Profesionales>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<Profesionales>));
                    }

                    profesionalService.updateBatch(entities);

                    entities.Clear();
                }

            return res;
        }

        [HttpGet, ActionName("pacientes")]
        public string pacientes(int id)
        {
            res = "Actualizando Pacientes";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/pacientes.json")))
            {
                tipo.Clear();
                List<Pacientes> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/pacientes.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<Pacientes>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<Pacientes>));
                }

                pacienteService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("analisissolicitudes")]
        public string analisissolicitudes(int id)
        {
            res = "Actualizando Solicitudes Analisis";

            if (File.Exists(HttpContext.Current.Server.MapPath("~/work/import/protocolos.json")))
            //if (File.Exists(getRuta() + "protocolos.json"))
            {

                tipo.Clear();
                List<AnalisisSolicitudes> entities;
                using (StreamReader file = File.OpenText(HttpContext.Current.Server.MapPath("~/work/import/protocolos.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<AnalisisSolicitudes>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<AnalisisSolicitudes>));
                }

                foreach (AnalisisSolicitudes c in entities)
                {
                    AnalisisSolicitudes entity = new AnalisisSolicitudes();
                    entity.nrosolicitud = c.nrosolicitud;
                    entity.protocolo = c.nrosolicitud;
                    entity.obrasocial = obrasocialService.find(c.codos);
                    entity.paciente = pacienteService.find(c.codpac);
                    entity.profesional = profesionalService.find(c.codprof);
                    entity.fecha = c.fecha;

                    // Practicas
                    foreach (AnalisisSolicitudesItems item in c.practicas)
                    {
                        AnalisisSolicitudesItems linea = new AnalisisSolicitudesItems();

                        linea.ID = new AnalisisSolicitudesItemsPK();
                        linea.ID.nrosolicitud = item.nrosolicitud;
                        linea.ID.items = item.items;

                        linea.practica = nomenclaturaService.find(item.codigo);
                        linea.nbu = nbuService.find(item.codigo);
                        linea.items = item.items;
                        linea.solicitud = entity;
                        linea.codigo = item.codigo;

                        // Plantilla asociada
                        string codigo = item.codigo;   // Verificamos equivalencia                        
                        Nbuinos nbuinos = solicitudService.getEquivalenciaPlantilla(item.codigo);
                        if (nbuinos != null) codigo = nbuinos.codigo;

                        // Resultados
                        if (item.resultadoitem != null)
                        {
                            foreach (AnalisisSolicitudesItemsResultado result in item.resultadoitem)
                            {
                                AnalisisSolicitudesItemsResultado lineares = new AnalisisSolicitudesItemsResultado();

                                lineares.ID = new AnalisisSolicitudesItemsResultadoPK();
                                lineares.ID.nrosolicitud = result.nrosolicitud;
                                lineares.ID.items = result.items;
                                lineares.ID.codigo = result.codigo;
                                lineares.ID.nroanalisis = result.nroanalisis;

                                lineares.resultado = result.resultado;
                                lineares.valoresn = result.valoresn;
                                lineares.solicituditem = linea;

                                PlantaAnalisisPK PKP = new PlantaAnalisisPK();
                                PKP.codigo = codigo;
                                PKP.items = result.items;

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

        [HttpGet, ActionName("nbu")]
        public string nbu(int id)
        {
            res = "Actualizando Nbu";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nbu.json")))
            {
                tipo.Clear();
                List<Nbu> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/nbu.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<Nbu>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<Nbu>));
                }

                nbuService.updateBatch(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("nbuinos")]
        public string nbuinos(int id)
        {
            res = "Actualizando Nbu INOS";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refinos.json")))
            {
                tipo.Clear();
                List<Nbuinos> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/refinos.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<Nbuinos>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<Nbuinos>));
                }

                solicitudService.saveNbuinos(entities);

                entities.Clear();
            }

            return res;
        }

        [HttpGet, ActionName("profesionalesusers")]
        public string profesionalesusers(int id)
        {
            res = "Actualizando Profesionales Users";

            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales_users.json")))
            {
                tipo.Clear();
                List<ProfesionalesUsers> entities;
                using (StreamReader file = File.OpenText(System.Web.HttpContext.Current.Server.MapPath("~/work/import/profesionales_users.json")))
                {
                    tipo.Append(file.ReadLine());
                    entities = (List<ProfesionalesUsers>)Newtonsoft.Json.JsonConvert.DeserializeObject(tipo.ToString(), typeof(List<ProfesionalesUsers>));
                }

                foreach (ProfesionalesUsers c in entities)
                {
                    if (profesionaluserService.find(c.idprof) == null)
                    {
                        ProfesionalesUsers entity = new ProfesionalesUsers();
                        entity.idprof = c.idprof;
                        entity.user = c.user;
                        entity.pass = c.pass;
                        entity.profesional = profesionalService.find(c.idprof);

                        profesionaluserService.persist(entity);
                    }
                }

                entities.Clear();
            }

            return res;
        }
    }
}
