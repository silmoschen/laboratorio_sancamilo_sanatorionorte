using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesSendResultsDao : IGenericDao<AnalisisSolicitudesSendResults, string>
    {        
        IList<AnalisisSolicitudesSendResults> getList(String nrosolicitud);
        IList<AnalisisSolicitudesSendResults> getUltimoEnvio(string nrosolicitud);
    }
}
