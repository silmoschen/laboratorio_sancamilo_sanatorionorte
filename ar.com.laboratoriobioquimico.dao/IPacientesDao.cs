using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesDao : IGenericDao<Pacientes, string>
    {
        IList<Pacientes> getAll(int pageNumber, int pageSize);
        IList<Pacientes> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
        Pacientes getByDocumento(string nrodoc);
    }
}
