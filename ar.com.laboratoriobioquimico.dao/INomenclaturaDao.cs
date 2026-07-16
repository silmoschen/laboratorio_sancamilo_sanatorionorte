using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface INomenclaturaDao : IGenericDao<Nomenclatura, string>
    {
        IList<Nomenclatura> getAll(int pageNumber, int pageSize);
        IList<Nomenclatura> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
