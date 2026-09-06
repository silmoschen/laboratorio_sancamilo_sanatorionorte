using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface INbuInternacionDao : IGenericDao<NbuInternacion, string>
    {
        IList<NbuInternacion> getAll(int pageNumber, int pageSize);
        IList<NbuInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
