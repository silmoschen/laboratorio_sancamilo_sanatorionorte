using ApplicationContext;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace laboratoriobioquimico.Controllers
{
    public class RestAnalisisResultadoController : ApiController
    {
        IAnalisisSolicitudesService entityService = (IAnalisisSolicitudesService)SpringContext.Instance.GetObject("AnalisisSolicitudesService");
        IAnalisisSolicitudesInternacionService solintService = (IAnalisisSolicitudesInternacionService)SpringContext.Instance.GetObject("AnalisisSolicitudesInternacionService");
        IReportsService reportService = (IReportsService)SpringContext.Instance.GetObject("ReportsService");

        //===================================================================================================

        public HttpResponseMessage result(ModelSolicitud model)
        {
            var response = new HttpResponseMessage();

            bool ress = false;

            if (model.tipo.Equals("A"))
            {

                AnalisisSolicitudes entity = entityService.find(model.nrosolicitud);
                foreach (AnalisisSolicitudesItems c in entity.practicas) c.Select = true;

                var list = reportService.getProtocoloResult(entity.nrosolicitud, entity.practicas);

                int i = 0;

                string codanter = "";

                string res = "<html><head><meta charset = 'utf-8'></head></html>";

                foreach (Reports r in list)
                {

                    ress = true;

                    if (i == 0)
                    {
                        res += "<hr/>";
                        res += "<table>";
                        res += "<tr>";
                        res += "<td valign='top'>Paciente: <b>" + r.Titulo10 + "</b></td>";
                        res += "<td valign='top'>Prot: <b>" + r.Titulo2 + "</b></td>";
                        res += "</tr>";

                        res += "<tr>";
                        res += "<td valign='top'>Prof.: <b>" + r.Titulo4 + "</b></td>";
                        res += "<td valign='top'>Fecha: <b>" + r.Titulo3 + "</b></td>";
                        res += "</tr>";

                        res += "</table>";

                        res += "<table>";
                    }

                    i = 1;

                    if (!r.Col1.Equals(codanter))
                    {
                        res += "<tr><td colspan='4'><hr/></td></tr>";
                        res += "<tr>";
                        res += "<td valign='top'  colspan='2' width='50%'></td>";
                        res += "<td valign='top' colspan='2' width='50%'></td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td valign='top' colspan='2' width='50%'><b>" + r.Col2 + "</b></td>";
                        res += "<td valign='top' colspan='2' width='50%'><i>" + r.Col9 + "</i></td>";
                        res += "</tr>";
                    }

                    if (r.Col3 == null) r.Col3 = "";
                    if (r.Col4 == null) r.Col4 = "";
                    if (r.Col5 == null) r.Col5 = "";
                    if (r.Col6 == null) r.Col6 = "";

                    res += "<tr>";
                    res += "<td valign='top'  width='25%'><b>" + r.Col3.TrimStart() + "</b></td>";
                    res += "<td valign='top'  width='25%' align='right'><i>" + r.Col4.TrimStart() + "</i></td>";
                    res += "<td valign='top' width='25%'><b>" + r.Col5.Trim() + "</b></td>";
                    res += "<td valign='top' width='25%' align='right'><i>" + r.Col6.TrimStart() + "</i></td>";
                    res += "</tr>";


                    codanter = r.Col1;

                }

                res += "</table></html>";

                if (!ress) res = "<h2>Sin Resultados</h2><br/><h3>Intente mas Tarde</h3>";

                response.Content = new StringContent(res);

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                return response;
            }

            //----------------------------------------------------------------------------------------------------

            if (model.tipo.Equals("I"))
            {

                AnalisisSolicitudesInternacion entity = solintService.find(model.nrosolicitud);
                foreach (AnalisisSolicitudesInternacionItems c in entity.practicas) c.Select = true;

                var list = reportService.getProtocoloResultInternacion(entity.nrosolicitud, entity.practicas);

                int i = 0;

                string codanter = "";

                string res = "<html><head><meta charset = 'utf-8'></head></html>";

                foreach (Reports r in list)
                {
                    ress = true;

                    if (i == 0)
                    {                        
                        res += "<table>";
                        res += "<tr>";
                        res += "<td valign='top'>Paciente: <b>" + r.Titulo10 + "</b></td>";
                        res += "<td valign='top'>Prot: <b>" + r.Titulo2 + "</b></td>";
                        res += "</tr>";

                        res += "<tr>";
                        res += "<td valign='top'>Prof.: <b>" + r.Titulo4 + "</b></td>";
                        res += "<td valign='top'>Fecha: <b>" + r.Titulo3 + "</b></td>";
                        res += "</tr>";

                        res += "</table>";

                        res += "<table>";
                    }

                    i = 1;

                    if (!r.Col1.Equals(codanter))
                    {
                        res += "<tr><td colspan='4'><hr/></td></tr>";
                        res += "<tr>";
                        res += "<td valign='top' colspan='2' width='50%'></td>";
                        res += "<td valign='top' colspan='2' width='50%'></td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td valign='top' colspan='2' width='50%'><b>" + r.Col2 + "</b></td>";
                        res += "<td valign='top' colspan='2' width='50%'><i>" + r.Col9 + "</i></td>";
                        res += "</tr>";
                    }

                    if (r.Col3 == null) r.Col3 = "";
                    if (r.Col4 == null) r.Col4 = "";
                    if (r.Col5 == null) r.Col5 = "";
                    if (r.Col6 == null) r.Col6 = "";

                    res += "<tr>";
                    res += "<td valign='top' width='25%'><b>" + r.Col3.TrimStart() + "</b></td>";
                    res += "<td valign='top' width='25%' align='right'><i>" + r.Col4.TrimStart() + "</i></td>";
                    res += "<td valign='top' width='25%'><b>" + r.Col5.Trim() + "</b></td>";
                    res += "<td valign='top' width='25%' align='right'><i>" + r.Col6.TrimStart() + "</i></td>";
                    res += "</tr>";


                    codanter = r.Col1;

                }

                res += "</table></html>";

                if (!ress) res = "<h2>Sin Resultados</h2><br/><h3>Intente mas Tarde</h3>";

                response.Content = new StringContent(res);                

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");

                return response;
            }

            return null;
        }

        //===================================================================================================
        
        [HttpPost, ActionName("resultado")]
        public HttpResponseMessage GetResultado([FromBody]ModelSolicitud model)
        {
            return result(model);
        }

        [HttpGet, ActionName("resultado")]
        public HttpResponseMessage getresultado(string key1, string key2)
        {
            ModelSolicitud model = new ModelSolicitud();

            model.nrosolicitud = key1;
            model.tipo = key2;

            return result(model);
        }        

    }
}
