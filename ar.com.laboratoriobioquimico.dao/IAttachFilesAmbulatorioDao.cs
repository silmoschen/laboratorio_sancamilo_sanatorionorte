using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAttachFilesAmbulatorioDao : IGenericDao<AttachFilesAmbulatorio, string>
    {
        IList<AttachFilesAmbulatorio> getAllProtocolo(string protocolo);
        IList<AttachFilesAmbulatorio> getAllFecha(DateTime fecha);

    }
}
