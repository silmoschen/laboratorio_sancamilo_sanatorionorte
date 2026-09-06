using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AnalisisSolicitudesInternacionSenddcm4cheDao : GenericDao<AnalisisSolicitudesInternacionSenddcm4che, string>, IAnalisisSolicitudesInternacionSenddcm4cheDao
    {

        public IList<AnalisisSolicitudesInternacionSenddcm4che> getList(string nrosolicitud)
        {
            object[] l = { nrosolicitud };
            return getAll("from AnalisisSolicitudesInternacionSenddcm4che c where c.solicitud.nrosolicitud = :p0 order by c.fechahora desc", l);
        }

    }
}