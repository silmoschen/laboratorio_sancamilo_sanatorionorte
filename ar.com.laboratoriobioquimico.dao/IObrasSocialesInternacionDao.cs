using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IObrasSocialesInternacionDao: IGenericDao<ObrasSocialesInternacion, string>
    {
        IList<ObrasSocialesInternacion> getAll(int pageNumber, int pageSize);
        IList<ObrasSocialesInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
