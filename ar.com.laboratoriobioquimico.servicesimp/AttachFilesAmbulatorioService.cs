using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class AttachFilesAmbulatorioService : IAttachFilesAmbulatorioService
    {
        private IAttachFilesAmbulatorioDao entityDao { get; set; }

        public AttachFilesAmbulatorio find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(AttachFilesAmbulatorio entity)
        {
            entityDao.persist(entity);
        }

        public string remove(AttachFilesAmbulatorio entity)
        {
            return entityDao.remove(entity);
        }

        public void removeFileProtocol(string protocol)
        {
            var list = entityDao.getAllProtocolo(protocol);

            foreach (AttachFilesAmbulatorio c in list) 
                entityDao.remove(c);
        }

        public IList<AttachFilesAmbulatorio> getAllProtocolo(string protocolo)
        {
            return entityDao.getAllProtocolo(protocolo);
        }
        
    }
}