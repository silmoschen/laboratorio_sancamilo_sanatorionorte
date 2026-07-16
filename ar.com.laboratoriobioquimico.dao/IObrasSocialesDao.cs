using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IObrasSocialesDao: IGenericDao<ObrasSociales, string>
    {
        IList<ObrasSociales> getAll(int pageNumber, int pageSize);
        IList<ObrasSociales> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
