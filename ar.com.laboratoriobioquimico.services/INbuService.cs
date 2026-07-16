using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INbuService
    {
        Nbu find(string id);
        void persist(Nbu entity);
        string remove(Nbu entity);
        IList<Nbu> getAll(int pageNumber, int pageSize);
        IList<Nbu> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Nbu> entities);
    }
}
