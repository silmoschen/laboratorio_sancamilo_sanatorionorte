using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IObrasSocialesDao: IGenericDao<ObrasSociales, string>
    {
        IList<ObrasSociales> getAll(int pageNumber, int pageSize);
        IList<ObrasSociales> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
