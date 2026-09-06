using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesInternacionDao : IGenericDao<PacientesInternacion, string>
    {
        IList<PacientesInternacion> getAll(int pageNumber, int pageSize);
        IList<PacientesInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
        PacientesInternacion getByDocumento(string nrodoc);
    }
}
