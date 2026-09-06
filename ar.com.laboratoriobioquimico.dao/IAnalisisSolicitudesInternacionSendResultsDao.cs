using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesInternacionSendResultsDao : IGenericDao<AnalisisSolicitudesInternacionSendResults, string>
    {        
        IList<AnalisisSolicitudesInternacionSendResults> getList(string nrosolicitud);     
    }
}
