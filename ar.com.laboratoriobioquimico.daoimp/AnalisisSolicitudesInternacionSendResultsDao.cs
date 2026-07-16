using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AnalisisSolicitudesInternacionSendResultsDao : GenericDao<AnalisisSolicitudesInternacionSendResults, string>, IAnalisisSolicitudesInternacionSendResultsDao
    {

        public IList<AnalisisSolicitudesInternacionSendResults> getList(String nrosolicitud)
        {
            object[] l = { nrosolicitud };
            return getAll("from AnalisisSolicitudesInternacionSendResults c where c.solicitud.nrosolicitud = :p0 order by c.fechahora desc", l);
        }

    }
}