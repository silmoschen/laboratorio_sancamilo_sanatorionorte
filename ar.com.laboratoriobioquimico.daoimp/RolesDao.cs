using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class RolesDao: GenericDao<Roles, long>, IRolesDao
    {
        
        public IList<Roles> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from Roles c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<Roles> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from Roles c where c.Descrip like " + "'" + find + "%' order by c.Descrip", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Roles c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Roles c", null);
        }

    }
}