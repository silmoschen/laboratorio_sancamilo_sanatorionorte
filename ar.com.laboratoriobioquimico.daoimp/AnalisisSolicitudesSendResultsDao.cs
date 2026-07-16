using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AnalisisSolicitudesSendResultsDao : GenericDao<AnalisisSolicitudesSendResults, string>, IAnalisisSolicitudesSendResultsDao
    {

        public IList<AnalisisSolicitudesSendResults> getList(String nrosolicitud)
        {
            object[] l = { nrosolicitud };
            return getAll("from AnalisisSolicitudesSendResults c where c.solicitud.nrosolicitud = :p0 order by c.fechahora desc", l);
        }

        public IList<AnalisisSolicitudesSendResults> getUltimoEnvio(string nrosolicitud)
        {
            object[] l = { nrosolicitud };
            return getAll("from AnalisisSolicitudesSendResults c where c.solicitud.nrosolicitud = :p0 order by c.fechahora desc", l, 0, 1);
        }

    }
}