using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AttachFilesInternacionDao : GenericDao<AttachFilesInternacion, string>, IAttachFilesInternacionDao
    {

        public IList<AttachFilesInternacion> getAllProtocolo(string protocolo)
        {
            object[] l = { protocolo };            
            return getListEntities("from AttachFilesInternacion c where c.Protocolo = :p0", l);
        }
        public IList<AttachFilesInternacion> getAllFecha(DateTime fecha)
        {
            object[] l = { fecha };
            return getListEntities("from AttachFilesInternacion c where c.Fecha = :p0", l);
        }

    }
}