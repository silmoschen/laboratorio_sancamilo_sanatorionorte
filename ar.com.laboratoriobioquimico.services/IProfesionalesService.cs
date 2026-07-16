using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesService
    {
        Profesionales find(string id);
        void persist(Profesionales entity);
        string remove(Profesionales entity);
        IList<Profesionales> getAll(int pageNumber, int pageSize);
        IList<Profesionales> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Profesionales> entities);
    }
}
