using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface INomenclaturaInternacionDao : IGenericDao<NomenclaturaInternacion, string>
    {
        IList<NomenclaturaInternacion> getAll(int pageNumber, int pageSize);
        IList<NomenclaturaInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
