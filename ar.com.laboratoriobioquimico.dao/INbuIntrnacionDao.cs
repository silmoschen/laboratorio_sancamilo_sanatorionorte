using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface INbuInternacionDao : IGenericDao<NbuInternacion, string>
    {
        IList<NbuInternacion> getAll(int pageNumber, int pageSize);
        IList<NbuInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
