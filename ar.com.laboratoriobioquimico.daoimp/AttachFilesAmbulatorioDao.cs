using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class AttachFilesAmbulatorioDao : GenericDao<AttachFilesAmbulatorio, string>, IAttachFilesAmbulatorioDao
    {

        public IList<AttachFilesAmbulatorio> getAllProtocolo(string protocolo)
        {
            object[] l = { protocolo };            
            return getListEntities("from AttachFilesAmbulatorio c where c.Protocolo = :p0", l);
        }
        public IList<AttachFilesAmbulatorio> getAllFecha(DateTime fecha)
        {
            object[] l = { fecha };
            return getListEntities("from AttachFilesAmbulatorio c where c.Fecha = :p0", l);
        }

    }
}