using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INomenclaturaService
    {
        Nomenclatura find(string id);
        void persist(Nomenclatura entity);
        string remove(Nomenclatura entity);
        IList<Nomenclatura> getAll(int pageNumber, int pageSize);
        IList<Nomenclatura> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<Nomenclatura> entities);
    }
}
