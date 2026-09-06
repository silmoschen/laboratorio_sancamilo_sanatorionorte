using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class QueryService : IQueryService
    {
        private IQueryDao entityDAO { get; set; }

        public IList<object[]> getListAnalisisAmbulatorioDia(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cant, substring(fecha, 7, 2) as dia, ANY_VALUE(substring(fecha, 5, 2)) as mes, ANY_VALUE(substring(fecha, 1, 4)) as anio from analisis_solicitudes where fecha between :p0 and :p1 group by substring(fecha, 7, 2) order by anio, mes, dia", l);
        }

        public IList<object[]> getListAnalisisInternacionDia(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cant, substring(fecha, 7, 2) as dia, ANY_VALUE(substring(fecha, 5, 2)) as mes, ANY_VALUE(substring(fecha, 1, 4)) as anio from analisis_solicitudes_internacion where fecha between :p0 and :p1 group by substring(fecha, 7, 2) order by anio, mes, dia", l);
        }

        public IList<object[]> getListAnalisisAmbulatorioMes(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cant, substring(fecha, 5, 2) as mes,  ANY_VALUE(substring(fecha, 1, 4)) as anio from analisis_solicitudes where fecha between :p0 and :p1 group by substring(fecha, 5, 2) order by anio, mes", l);
        }

        public IList<object[]> getListAnalisisInternacionMes(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cant, substring(fecha, 5, 2) as mes,  ANY_VALUE(substring(fecha, 1, 4)) as anio from analisis_solicitudes_internacion where fecha between :p0 and :p1 group by substring(fecha, 5, 2) order by anio, mes", l);
        }

        public List<string> getListProfesionales()
        {
            var list = new List<string>();
            List<object[]> r = entityDAO.query("select idprof, idprof from facturacion_profesionales", null);

            foreach (object[] row in r) list.Add(row[0].ToString());

            if (r.Count == 0) return null;

            return list;

        }

        // 05-05-2020 => Estadisticas =======================================================================

        public IList<object[]> getListCantidadPacientesInternadosMes(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cantidad, ANY_VALUE(concat(substring(fecha, 5, 2), '/', substring(fecha, 1, 4))) as periodo, ANY_VALUE(concat(substring(fecha, 1, 4), substring(fecha, 5, 2))) as fecha from analisis_solicitudes_internacion where fecha between :p0 and :p1 group by substring(fecha, 1, 6) order by substring(fecha, 1, 6)", l);
        }

        public IList<object[]> getListCantidadPacientesAmbulatorioMes(string desde, string hasta)
        {
            object[] l = { desde, hasta };
            return entityDAO.query("select count(*) as cantidad, ANY_VALUE(concat(substring(fecha, 5, 2), '/', substring(fecha, 1, 4))) as periodo, ANY_VALUE(concat(substring(fecha, 1, 4), substring(fecha, 5, 2))) as fecha from analisis_solicitudes where fecha between :p0 and :p1 group by substring(fecha, 1, 6) order by substring(fecha, 1, 6)", l);
        }

        public long? getDeterminacionesInternacion(string fecha)
        {
            object[] l = { fecha };
            return (long) entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes_items_internacion, analisis_solicitudes_internacion where analisis_solicitudes_items_internacion.nrosolicitud = analisis_solicitudes_internacion.nrosolicitud and  substring(analisis_solicitudes_internacion.fecha, 1, 6) = :p0", l);
        }

        public long? getDeterminacionesAmbulatorio(string fecha)
        {
            object[] l = { fecha };
            return (long)entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes_items, analisis_solicitudes where analisis_solicitudes_items.nrosolicitud = analisis_solicitudes.nrosolicitud and  substring(analisis_solicitudes.fecha, 1, 6) = :p0", l);
        }

        //----------------------------------------------------------------------------------------------------------------

        public IList<object[]> getListPracticasInternacion(string periodo)
        {
            object[] l = { periodo };
            return entityDAO.query("select distinct(codigo) as codigo, codigo from analisis_solicitudes_items_internacion, analisis_solicitudes_internacion where analisis_solicitudes_items_internacion.nrosolicitud = analisis_solicitudes_internacion.nrosolicitud and analisis_solicitudes_internacion.fecha and substring(analisis_solicitudes_internacion.fecha, 1, 6) = :p0 order by codigo", l);
        }

        public long? getDeterminacionesInternacion(string fecha, string codigo)
        {
            object[] l = { fecha, codigo };
            return (long)entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes_items_internacion, analisis_solicitudes_internacion where analisis_solicitudes_items_internacion.nrosolicitud = analisis_solicitudes_internacion.nrosolicitud and  substring(analisis_solicitudes_internacion.fecha, 1, 6) = :p0 and analisis_solicitudes_items_internacion.codigo = :p1", l);
        }

        public IList<object[]> getListPracticasAmbulatorio(string periodo)
        {
            object[] l = { periodo };
            return entityDAO.query("select distinct(codigo) as codigo, codigo from analisis_solicitudes_items, analisis_solicitudes where analisis_solicitudes_items.nrosolicitud = analisis_solicitudes.nrosolicitud and substring(analisis_solicitudes.fecha, 1, 6) = :p0 order by codigo", l);
        }

        public long? getDeterminacionesAmbulatorio(string fecha, string codigo)
        {
            object[] l = { fecha, codigo };
            return (long)entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes_items, analisis_solicitudes where analisis_solicitudes_items.nrosolicitud = analisis_solicitudes.nrosolicitud and  substring(analisis_solicitudes.fecha, 1, 6) = :p0 and analisis_solicitudes_items.codigo = :p1", l);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public IList<object[]> getListObrasSocialesInternadosMes(string periodo)
        {
            object[] l = { periodo };
            return entityDAO.query("select distinct(fk_obsocial), fk_obsocial from analisis_solicitudes_internacion where analisis_solicitudes_internacion.fecha and substring(analisis_solicitudes_internacion.fecha, 1, 6) = :p0 order by fk_obsocial", l);
        }

        public long? getListPracticasObrasSocialesInternadosMes(string periodo, string codos)
        {
            object[] l = { periodo, codos }; 
            return (long)entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes_internacion, analisis_solicitudes_items_internacion where analisis_solicitudes_internacion.nrosolicitud = analisis_solicitudes_items_internacion.nrosolicitud and substring(analisis_solicitudes_internacion.fecha, 1, 6) = :p0 and analisis_solicitudes_internacion.fk_obsocial = :p1", l);
        }

        public IList<object[]> getListObrasSocialesAmbulatorioMes(string periodo)
        {
            object[] l = { periodo };
            return entityDAO.query("select distinct(fk_obsocial), fk_obsocial from analisis_solicitudes where analisis_solicitudes.fecha and substring(analisis_solicitudes.fecha, 1, 6) = :p0 order by fk_obsocial", l);
        }

        public long? getListPracticasObrasSocialesAmbulatorioMes(string periodo, string codos)
        {
            object[] l = { periodo, codos };
            return (long)entityDAO.getNativeSQL("select count(*) as cantidad from analisis_solicitudes, analisis_solicitudes_items where analisis_solicitudes.nrosolicitud = analisis_solicitudes_items.nrosolicitud and substring(analisis_solicitudes.fecha, 1, 6) = :p0 and analisis_solicitudes.fk_obsocial = :p1", l);
        }

    }
}