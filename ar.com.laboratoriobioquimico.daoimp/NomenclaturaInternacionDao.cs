using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class NomenclaturaInternacionDao: CatalogDao<NomenclaturaInternacion, string>, INomenclaturaInternacionDao
    {
        
        public IList<NomenclaturaInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from NomenclaturaInternacion c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<NomenclaturaInternacion> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from NomenclaturaInternacion c where c.Descrip like " + "'" + find + "%' order by c.Descrip", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from NomenclaturaInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from NomenclaturaInternacion c", null);
        }

    }
}