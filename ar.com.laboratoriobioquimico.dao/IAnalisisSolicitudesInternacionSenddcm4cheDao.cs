using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAnalisisSolicitudesInternacionSenddcm4cheDao : IGenericDao<AnalisisSolicitudesInternacionSenddcm4che, string>
    {        
        IList<AnalisisSolicitudesInternacionSenddcm4che> getList(string nrosolicitud);     
    }
}
