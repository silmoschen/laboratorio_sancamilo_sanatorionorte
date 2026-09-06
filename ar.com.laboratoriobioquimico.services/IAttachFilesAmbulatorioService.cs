using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IAttachFilesAmbulatorioService
    {
        AttachFilesAmbulatorio find(string id);
        void persist(AttachFilesAmbulatorio entity);
        string remove(AttachFilesAmbulatorio entity);
        void removeFileProtocol(string protocol);
        IList<AttachFilesAmbulatorio> getAllProtocolo(string protocolo);
    }
}
