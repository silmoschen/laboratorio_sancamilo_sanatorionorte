using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesSendResultsDao : IGenericDao<AnalisisSolicitudesSendResults, string>
    {        
        IList<AnalisisSolicitudesSendResults> getList(string nrosolicitud);
        IList<AnalisisSolicitudesSendResults> getUltimoEnvio(string nrosolicitud);
    }
}
