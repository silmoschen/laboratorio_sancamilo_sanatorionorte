using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesInternacionDao : IGenericDao<PacientesInternacion, string>
    {
        IList<PacientesInternacion> getAll(int pageNumber, int pageSize);
        IList<PacientesInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
        PacientesInternacion getByDocumento(string nrodoc);
    }
}
