using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesInternacionDao : IGenericDao<ProfesionalesInternacion, string>
    {
        IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize);
        IList<ProfesionalesInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
