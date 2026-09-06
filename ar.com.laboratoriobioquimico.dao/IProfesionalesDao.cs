using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesDao : IGenericDao<Profesionales, string>
    {
        IList<Profesionales> getAll(int pageNumber, int pageSize);
        IList<Profesionales> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
