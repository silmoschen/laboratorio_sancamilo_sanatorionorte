using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.rest.Models;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IFacturacionesProfesionalService
    {
        string getUrl();
        List<string> getProfesionales();
        IList<FacturacionDetalleFact> getFacturaciones(ParametrosDetalleFact parametros);
        IList<FacturacionDetalleFact> getTotalesFacturados(ParametrosDetalleFact parametros);
    }
}
