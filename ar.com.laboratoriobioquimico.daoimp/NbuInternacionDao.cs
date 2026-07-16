using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class NbuInternacionDao: GenericDao<NbuInternacion, string>, INbuInternacionDao
    {
        
        public IList<NbuInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from NbuInternacion c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<NbuInternacion> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from NbuInternacion c where c.Descrip like " + "'" + find + "%' order by c.Descrip", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from NbuInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from NbuInternacion c", null);
        }

    }
}