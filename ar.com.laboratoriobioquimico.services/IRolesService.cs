using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IRolesService
    {
        Roles find(long id);
        void persist(Roles entity);
        string remove(Roles entity);
        IList<Roles> getAll(int pageNumber, int pageSize);
        IList<Roles> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
