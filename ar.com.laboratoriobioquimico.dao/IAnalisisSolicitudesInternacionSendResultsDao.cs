using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesInternacionSendResultsDao : IGenericDao<AnalisisSolicitudesInternacionSendResults, string>
    {        
        IList<AnalisisSolicitudesInternacionSendResults> getList(String nrosolicitud);     
    }
}
