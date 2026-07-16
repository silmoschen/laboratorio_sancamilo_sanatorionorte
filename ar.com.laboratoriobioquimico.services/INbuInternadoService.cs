using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INbuInternacionService
    {
        NbuInternacion find(string id);
        void persist(NbuInternacion entity);
        string remove(NbuInternacion entity);
        IList<NbuInternacion> getAll(int pageNumber, int pageSize);
        IList<NbuInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<NbuInternacion> entities);
    }
}
