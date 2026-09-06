using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IRolesService
    {
        Roles find(long id);
        void persist(Roles entity);
        string remove(Roles entity);
        IList<Roles> getAll(int pageNumber, int pageSize);
        IList<Roles> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
