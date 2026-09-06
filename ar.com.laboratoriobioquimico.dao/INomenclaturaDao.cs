using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface INomenclaturaDao : IGenericDao<Nomenclatura, string>
    {
        IList<Nomenclatura> getAll(int pageNumber, int pageSize);
        IList<Nomenclatura> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
