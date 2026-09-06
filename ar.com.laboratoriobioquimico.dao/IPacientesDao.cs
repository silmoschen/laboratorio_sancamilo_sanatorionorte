using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesDao : IGenericDao<Pacientes, string>
    {
        IList<Pacientes> getAll(int pageNumber, int pageSize);
        IList<Pacientes> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
        Pacientes getByDocumento(string nrodoc);
    }
}
