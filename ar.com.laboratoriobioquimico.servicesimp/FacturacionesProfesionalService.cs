using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class FacturacionesProfesionalService : IFacturacionesProfesionalService
    {
        private IQueryService queryService { get; set; }

        static RestApi rest = new RestApi();        

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
            var r = rest.Post(this.getUrl(), "/api/facturaciondetalle/periodosfacturados", parametros).ToString();

            var entities = (List<FacturacionDetalleFact>)Newtonsoft.Json.JsonConvert.DeserializeObject(r.ToString(), typeof(List<FacturacionDetalleFact>));

            return entities;
        }

        public IList<FacturacionDetalleFact> getTotalesFacturados(ParametrosDetalleFact parametros)
        {
            parametros.profesionales = this.getProfesionales();

            var r = rest.Post(this.getUrl(), "/api/facturaciondetalle/totalesobrassociales", parametros);

            var entities = (List<FacturacionDetalleFact>)Newtonsoft.Json.JsonConvert.DeserializeObject(r.ToString(), typeof(List<FacturacionDetalleFact>));

            return entities;
        }

    }
}