using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesInternacionService
    {
        ProfesionalesInternacion find(string id);
        void persist(ProfesionalesInternacion entity);
        string remove(ProfesionalesInternacion entity);
        IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize);
        IList<ProfesionalesInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<ProfesionalesInternacion> entities);
    }
}
