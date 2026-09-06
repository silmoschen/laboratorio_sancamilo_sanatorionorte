using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INbuInternacionService
    {
        NbuInternacion find(string id);
        void persist(NbuInternacion entity);
        string remove(NbuInternacion entity);
        IList<NbuInternacion> getAll(int pageNumber, int pageSize);
        IList<NbuInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<NbuInternacion> entities);
    }
}
