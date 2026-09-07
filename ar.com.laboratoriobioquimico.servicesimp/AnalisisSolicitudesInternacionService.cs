using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class AnalisisSolicitudesInternacionService : IAnalisisSolicitudesInternacionService
    {
        private IAnalisisSolicitudesInternacionDao entityDao { get; set; }
        private IAnalisisSolicitudesInternacionSendResultsDao sendmailDao { get; set; }
        private IAnalisisSolicitudesInternacionSenddcm4cheDao senddcm4cheDao { get; set; }        
        private INbuinosInternacionDao nbuinosDao { get; set; }
        private IPacientesInternacionDao pacienteDao { get; set; }
        private IParametrosDao parametroDao { get; set; }

        private string rutaSincro = "";

        public AnalisisSolicitudesInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(AnalisisSolicitudesInternacion entity)
        {
            entityDao.persist(entity);
        }

        public void merge(AnalisisSolicitudesInternacion entity)
        {
            entityDao.merge(entity);
        }

        public void update(AnalisisSolicitudesInternacion entity)
        {
            entityDao.update(entity);
        }

        public string remove(AnalisisSolicitudesInternacion entity)
        {
            return entityDao.remove(entity);
        }
        public IList<AnalisisSolicitudesInternacion> getAll(int? pageNumber, int? pageSize, IList<string> orderByDesc)
        {
            Parametros p = parametroDao.get(1);

            var l = entityDao.getAll(pageNumber, pageSize, null, null, null, orderByDesc);

            foreach (var o in l)
            {
                o.dcm4che = p.Opt1;
                o.logsDcm4che = senddcm4cheDao.getList(o.nrosolicitud);
            }

            return l;
        }

        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

        public void updateBatch(AnalisisSolicitudesInternacion entity)
        {
            //if (entityDao.get(entity.nrosolicitud) == null) entityDao.save(entity); else entityDao.merge(entity);
            var c = entityDao.get(entity.nrosolicitud);
            if (c == null) entityDao.save(entity);
            else
            {
                entityDao.remove(c);
                entityDao.save(entity);
            }
        }

        public void deleteBatch(IList<AnalisisSolicitudesInternacion> entities)
        {
            entityDao.removeBatch(entities);
        }

        public IList<AnalisisSolicitudesInternacion> getListWithResults(int size)
        {
            return entityDao.getListWithResults(size);
        }

        public IList<Solicitudes> getListSolicitudesResults(int size)
        {
            List<object[]> rows = entityDao.getListSolicitudesResults(size);

            var list = new List<Solicitudes>();

            foreach (object[] row in rows)
            {
                var c = new Solicitudes();
                c.nrosolicitud = row[0].ToString();
                c.version = row[1].ToString();

                list.Add(c);
            }

            return list;
        }

        public IList<AnalisisSolicitudesInternacionItemsResultado> getSolicitudResults(string nrosolicitud)
        {
            var resultados = new List<AnalisisSolicitudesInternacionItemsResultado>();

            List<object[]> rows = entityDao.getSolicitudResults(nrosolicitud);

            foreach (object[] row in rows)
            {
                var r = new AnalisisSolicitudesInternacionItemsResultado();
                r.nrosolicitud = row[0].ToString();
                r.codigo = row[1].ToString();
                if (row[2] != null) r.items = row[2].ToString();
                if (row[3] != null) r.resultado = row[3].ToString();
                if (row[4] != null) r.valoresn = row[4].ToString();
                if (row[5] != null) r.ID.nroanalisis = row[5].ToString();
                if (row[6] != null) r.version = row[6].ToString();
                if (row[7] != null) r.plantillaID = row[7].ToString();
                if (row[8] != null) r.observaciones = row[8].ToString();

                resultados.Add(r);
            }

            return resultados;
        }

        public IList<AnalisisSolicitudesInternacion> getListProtocolosPaciente(PacientesInternacion paciente, IList<string> orderByDesc)
        {
            return entityDao.getAll(null, null, paciente, null, null, orderByDesc);
        }

        public NbuinosInternacion getEquivalenciaPlantilla(string codigo)
        {            
            return nbuinosDao.get(codigo);
        }

        public void saveNbuinos(List<NbuinosInternacion> entities)
        {
            foreach (NbuinosInternacion c in entities)
                if (nbuinosDao.get(c.codnbu) == null) nbuinosDao.save(c); else nbuinosDao.merge(c);
        }

        public IList<AnalisisSolicitudesInternacion> getListSolicitudesPaciente(string nrodoc)
        {
            PacientesInternacion paciente = pacienteDao.getByDocumento(nrodoc);

            if (paciente == null) return null;

            var entities = new List<AnalisisSolicitudesInternacion>();

            List<object[]> rows = entityDao.getListSolicitudesPaciente(paciente.codpac);

            foreach (object[] row in rows)
            {
                var r = new AnalisisSolicitudesInternacion();
                r.nrosolicitud = row[0].ToString();                
                if (row[1] != null) r.fecha = row[1].ToString();
                r.paciente = paciente;
                entities.Add(r);
            }

            return entities;
        }
        public void saveSendMail(AnalisisSolicitudesInternacionSendResults entity)
        {
            sendmailDao.persist(entity);
        }

        public IList<AnalisisSolicitudesInternacionSendResults> getListSendResult(string nrosolicitud)
        {
            return sendmailDao.getList(nrosolicitud);
        }

        public IList<AnalisisSolicitudesInternacion> getListProtocolosFecha(string desde, string hasta, IList<string> orderByDesc)
        {
            return entityDao.getAll(null, null, null, desde, hasta, orderByDesc);
        }

        public string getRutaSincro()
        {
            return rutaSincro;
        }

        public void saveSenddcm4che(AnalisisSolicitudesInternacionSenddcm4che entity)
        {
            parametroDao.get(1);
            senddcm4cheDao.persist(entity);
        }

        public IList<AnalisisSolicitudesInternacionSenddcm4che> getListSenddcm4che(string nrosolicitud)
        {
            return senddcm4cheDao.getList(nrosolicitud);
        }
    }
}