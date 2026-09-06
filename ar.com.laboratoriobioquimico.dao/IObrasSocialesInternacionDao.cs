using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IObrasSocialesInternacionDao: IGenericDao<ObrasSocialesInternacion, string>
    {
        IList<ObrasSocialesInternacion> getAll(int pageNumber, int pageSize);
        IList<ObrasSocialesInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
