using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAttachFilesInternacionDao : IGenericDao<AttachFilesInternacion, string>
    {
        IList<AttachFilesInternacion> getAllProtocolo(string protocolo);
        IList<AttachFilesInternacion> getAllFecha(DateTime fecha);

    }
}
