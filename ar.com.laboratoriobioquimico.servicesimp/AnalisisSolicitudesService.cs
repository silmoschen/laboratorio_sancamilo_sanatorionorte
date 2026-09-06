using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using laboratoriobioquimico.Models;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class AnalisisSolicitudesService : IAnalisisSolicitudesService
    {
        private IAnalisisSolicitudesDao entityDao { get; set; }
        private IAnalisisSolicitudesSendResultsDao sendmailDao { get; set; }
        private INbuinosDao nbuinosDao { get; set; }
        private IPacientesDao pacienteDao { get; set; }
        private IAttachFilesAmbulatorioDao attachFilesDao { get; set; }

        private string rutaSincro = "";
        public AnalisisSolicitudes find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(AnalisisSolicitudes entity)
        {
            entityDao.persist(entity);
        }

        public void merge(AnalisisSolicitudes entity)
        {
            entityDao.merge(entity);
        }

        public void update(AnalisisSolicitudes entity)
        {
            entityDao.update(entity);
        }

        public string remove(AnalisisSolicitudes entity)
        {
            return entityDao.remove(entity);
        }
        public IList<AnalisisSolicitudes> getAll(int? pageNumber, int? pageSize, IList<string> orderByDesc)
        {
            var list = entityDao.getAll(pageNumber, pageSize, null, null, null, orderByDesc);

            foreach (var item in list)
            {
                var it = sendmailDao.getUltimoEnvio(item.nrosolicitud);

                if (it != null)
                    if (it.Count > 0) item.mensaje = "Enviado: " + it[0].fechahora; else item.mensaje = null;                

            }

            return loadAttachFiles(list);            
        }

        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

        public void updateBatch(AnalisisSolicitudes entity)
        {
            var c = entityDao.get(entity.nrosolicitud);
            if (c == null) entityDao.save(entity);
            else
            {
                entityDao.remove(c);
                entityDao.save(entity);
            }
        }

        public void deleteBatch(IList<AnalisisSolicitudes> entities)
        {
            entityDao.removeBatch(entities);
        }

        public IList<AnalisisSolicitudes> getListWithResults(int size)
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

        /*
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
        */

        public IList<AnalisisSolicitudesItemsResultado> getSolicitudResults(string nrosolicitud)
        {
            var resultados = new List<AnalisisSolicitudesItemsResultado>();

            List<object[]> rows = entityDao.getSolicitudResults(nrosolicitud);

            foreach (object[] row in rows)
            {
                var r = new AnalisisSolicitudesItemsResultado();
                r.nrosolicitud = row[0].ToString();
                r.codigo = row[1].ToString();
                if (row[2] != null) r.items = row[2].ToString();
                if (row[3] != null) r.resultado = row[3].ToString();
                if (row[4] != null) r.valoresn = row[4].ToString();
                if (row[5] != null) r.nroanalisis = row[5].ToString();
                if (row[6] != null) r.version = row[6].ToString();
                if (row[7] != null) r.plantillaID = row[7].ToString();
                if (row[8] != null) r.observaciones = row[8].ToString();

                resultados.Add(r);
            }

            return resultados;
        }

        public IList<AnalisisSolicitudes> getListProtocolosPaciente(Pacientes paciente, IList<string> orderByDesc)
        {
            return entityDao.getAll(null, null, paciente, null, null, orderByDesc);
        }

        public Nbuinos getEquivalenciaPlantilla(string codigo)
        {
            return nbuinosDao.get(codigo);
        }

        public void saveNbuinos(List<Nbuinos> entities)
        {
            foreach (Nbuinos c in entities)
                if (nbuinosDao.get(c.codnbu) == null) nbuinosDao.save(c); else nbuinosDao.merge(c);
        }

        public IList<AnalisisSolicitudes> getListSolicitudesPaciente(string nrodoc)
        {
            Pacientes paciente = pacienteDao.getByDocumento(nrodoc);

            if (paciente == null) return null;

            var entities = new List<AnalisisSolicitudes>();

            List<object[]> rows = entityDao.getListSolicitudesPaciente(paciente.codpac);

            foreach (object[] row in rows)
            {
                var r = new AnalisisSolicitudes();
                r.nrosolicitud = row[0].ToString();                
                if (row[1] != null) r.fecha = row[1].ToString();
                r.paciente = paciente;
                entities.Add(r);
            }

            return loadAttachFiles(entities);
            
        }

        public void saveSendMail(AnalisisSolicitudesSendResults entity)
        {
            sendmailDao.persist(entity);
        }

        public IList<AnalisisSolicitudesSendResults> getListSendResult(string nrosolicitud)
        {
            return sendmailDao.getList(nrosolicitud);
        }

        public IList<AnalisisSolicitudes> getListProtocolosFecha(string desde, string hasta, IList<string> orderByDesc)
        {
            var list = entityDao.getAll(null, null, null, desde, hasta, orderByDesc);            
            return loadAttachFiles(list);
        }

        public string getRutaSincro()
        {
            return rutaSincro;
        }

        public IList<AnalisisSolicitudes> getAll(int? pageNumber, int? pageSize, Pacientes paciente, IList<string> orderByDesc)
        {
            var list = entityDao.getAll(pageNumber, pageSize, paciente, null, null, orderByDesc);
            return loadAttachFiles(list);
        }

        public long getTotalRegistros(Pacientes paciente)
        {
            return entityDao.getTotalRegistros(paciente);
        }

        //============================================================================================

        private IList<AnalisisSolicitudes> loadAttachFiles(IList<AnalisisSolicitudes> entities)  
        {
            foreach (AnalisisSolicitudes item in entities)
            {
                var files = attachFilesDao.getAllProtocolo(item.nrosolicitud);

                foreach (AttachFilesAmbulatorio c in files) item.archivosAdjuntos.Add(c);
            }

            return entities;
        }

        //============================================================================================
    }
}