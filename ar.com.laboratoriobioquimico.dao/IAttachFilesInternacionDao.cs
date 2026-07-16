using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IAttachFilesInternacionDao : IGenericDao<AttachFilesInternacion, string>
    {
        IList<AttachFilesInternacion> getAllProtocolo(string protocolo);
        IList<AttachFilesInternacion> getAllFecha(DateTime fecha);

    }
}
