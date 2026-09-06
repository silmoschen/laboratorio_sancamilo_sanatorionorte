using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IAttachFilesInternacionService
    {
        AttachFilesInternacion find(string id);
        void persist(AttachFilesInternacion entity);
        string remove(AttachFilesInternacion entity);
        void removeFileProtocol(string protocol);
        IList<AttachFilesInternacion> getAllProtocolo(string protocolo);
    }
}
