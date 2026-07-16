using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesDao : IGenericDao<Profesionales, string>
    {
        IList<Profesionales> getAll(int pageNumber, int pageSize);
        IList<Profesionales> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
