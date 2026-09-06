using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesInternacionService
    {
        ProfesionalesInternacion find(string id);
        void persist(ProfesionalesInternacion entity);
        string remove(ProfesionalesInternacion entity);
        IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize);
        IList<ProfesionalesInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<ProfesionalesInternacion> entities);
    }
}
