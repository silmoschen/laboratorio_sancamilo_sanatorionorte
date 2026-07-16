using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesInternacionDao : IGenericDao<ProfesionalesInternacion, string>
    {
        IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize);
        IList<ProfesionalesInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
