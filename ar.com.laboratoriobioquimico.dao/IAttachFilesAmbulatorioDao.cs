using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAttachFilesAmbulatorioDao : IGenericDao<AttachFilesAmbulatorio, string>
    {
        IList<AttachFilesAmbulatorio> getAllProtocolo(string protocolo);
        IList<AttachFilesAmbulatorio> getAllFecha(DateTime fecha);

    }
}
