using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class NbuDao: GenericDao<Nbu, string>, INbuDao
    {
        
        public IList<Nbu> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from Nbu c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<Nbu> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from Nbu c where c.Descrip like " + "'" + find + "%' order by c.Descrip", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Nbu c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Nbu c", null);
        }

    }
}