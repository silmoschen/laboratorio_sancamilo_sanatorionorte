using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class AttachFilesInternacionService : IAttachFilesInternacionService
    {
        private IAttachFilesInternacionDao entityDao { get; set; }

        public AttachFilesInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(AttachFilesInternacion entity)
        {
            entityDao.persist(entity);
        }

        public string remove(AttachFilesInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public void removeFileProtocol(string protocol)
        {
            var list = entityDao.getAllProtocolo(protocol);

            foreach (AttachFilesInternacion c in list) 
                entityDao.remove(c);
        }

        public IList<AttachFilesInternacion> getAllProtocolo(string protocolo)
        {
            return entityDao.getAllProtocolo(protocolo);
        }

    }
}