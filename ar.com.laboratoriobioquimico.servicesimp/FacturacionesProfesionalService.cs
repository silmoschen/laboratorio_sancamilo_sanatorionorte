using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;
using RestSharp;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class FacturacionesProfesionalService : IFacturacionesProfesionalService
    {
        private IQueryService queryService { get; set; }

        public string url { get; set; }
        public string getUrl()
        {
            return this.url;
        }

        public List<string> getProfesionales()
        {
            return queryService.getListProfesionales();
        }

        public IList<FacturacionDetalleFact> getFacturaciones(ParametrosDetalleFact parametros)
        {
            var r = PostJson(this.getUrl(), "/api/facturaciondetalle/periodosfacturados", parametros);

            var entities = (List<FacturacionDetalleFact>)Newtonsoft.Json.JsonConvert.DeserializeObject(r.ToString(), typeof(List<FacturacionDetalleFact>));

            return entities;
        }

        public IList<FacturacionDetalleFact> getTotalesFacturados(ParametrosDetalleFact parametros)
        {
            parametros.profesionales = this.getProfesionales();

            var r = PostJson(this.getUrl(), "/api/facturaciondetalle/totalesobrassociales", parametros);

            var entities = (List<FacturacionDetalleFact>)Newtonsoft.Json.JsonConvert.DeserializeObject(r.ToString(), typeof(List<FacturacionDetalleFact>));

            return entities;
        }

        private static string PostJson(string restUrl, string metodo, object json)
        {
            RestClient client = new RestClient(restUrl + metodo);
            RestRequest request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(json);
            return client.Execute(request).Content;
        }

    }
}