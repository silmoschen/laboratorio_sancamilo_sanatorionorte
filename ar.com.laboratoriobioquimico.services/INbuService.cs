using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INbuService
    {
        Nbu find(string id);
        void persist(Nbu entity);
        string remove(Nbu entity);
        IList<Nbu> getAll(int pageNumber, int pageSize);
        IList<Nbu> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Nbu> entities);
    }
}
