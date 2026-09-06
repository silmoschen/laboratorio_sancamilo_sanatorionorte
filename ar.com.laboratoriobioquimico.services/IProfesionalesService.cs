using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesService
    {
        Profesionales find(string id);
        void persist(Profesionales entity);
        string remove(Profesionales entity);
        IList<Profesionales> getAll(int pageNumber, int pageSize);
        IList<Profesionales> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Profesionales> entities);
    }
}
