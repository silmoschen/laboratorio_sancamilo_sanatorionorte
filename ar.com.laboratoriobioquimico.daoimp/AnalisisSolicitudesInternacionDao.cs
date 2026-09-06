using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using NHibernate.Criterion;
using NHibernate;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AnalisisSolicitudesInternacionDao: GenericDao<AnalisisSolicitudesInternacion, string>, IAnalisisSolicitudesInternacionDao
    {

        public IList<AnalisisSolicitudesInternacion> getAll(int? pageNumber, int? pageSize, PacientesInternacion paciente, string desde, string hasta, IList<string> orderByDesc)
        {
            List<AnalisisSolicitudesInternacion> list = new List<AnalisisSolicitudesInternacion>();

            ICriteria criteria = session.CreateCriteria("AnalisisSolicitudesInternacion");
                        
            if (paciente != null) criteria.Add(Restrictions.Eq("paciente", paciente));
            if (desde != null) criteria.Add(Restrictions.Ge("fecha", desde));
            if (hasta != null) criteria.Add(Restrictions.Le("fecha", hasta));
            
            if (pageNumber != null) criteria.SetFirstResult((pageNumber.Value - 1) * pageSize.Value);
            if (pageSize != null) criteria.SetMaxResults(pageSize.Value);

            if (orderByDesc != null)
                foreach (string c in orderByDesc) criteria.AddOrder(Order.Desc(c));

            criteria.SetCacheable(true).SetFlushMode(FlushMode.Auto);

            criteria.List(list);
            return list;

        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from AnalisisSolicitudesInternacion c", pageSize);
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from AnalisisSolicitudesInternacion c", null);
        }

        public IList<AnalisisSolicitudesInternacion> getListWithResults(int size)
        {
            return getAll("from AnalisisSolicitudesInternacion c where version is not null order by c.fecha, c.nrosolicitud desc", 0, size);
        }

        public List<object[]> getListSolicitudesResults(int size) {
            List<object[]> rows = getListNativeSQL("select nrosolicitud, version from analisis_solicitudes_internacion where version is not null order by fecha, nrosolicitud", 0, size);
            return rows; 
        }

        public List<object[]> getSolicitudResults(string nrosolicitud)
        {
            object[] l = { nrosolicitud };
            List<object[]> rows = getListNativeSQL("select nrosolicitud, codigo, items, resultado, valoresn, nroanalisis, version, fk_items_plantilla, observaciones from analisis_solicitudes_items_resultado_internacion " +
                "where nrosolicitud = :p0 order by nroanalisis, codigo, items", l);
            return rows;
        }

        public List<object[]> getListSolicitudesPaciente(string codpac)
        {
            object[] l = { codpac };
            List<object[]> rows = getListNativeSQL("select nrosolicitud, fecha, fk_paciente from analisis_solicitudes_internacion where fk_paciente = :p0 order by fecha, nrosolicitud", l);
            return rows;
        }

    }
}