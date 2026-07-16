using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPacientesInternacionService
    {
        PacientesInternacion find(string id);
        void persist(PacientesInternacion entity);
        string remove(PacientesInternacion entity);
        IList<PacientesInternacion> getAll(int pageNumber, int pageSize);
        IList<PacientesInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<PacientesInternacion> entities);
        PacientesInternacion findByNrodoc(string nrodoc);
        PacientesInternacionMails findPacienteMail(PacientesInternacion entity);
        void saveMail(PacientesInternacionMails entity);
    }
}
