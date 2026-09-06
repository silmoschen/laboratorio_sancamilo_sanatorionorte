using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class NomenclaturaDao: GenericDao<Nomenclatura, string>, INomenclaturaDao
    {
        
        public IList<Nomenclatura> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from Nomenclatura c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<Nomenclatura> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from Nomenclatura c where c.Descrip like " + "'" + find + "%' order by c.Descrip", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Nomenclatura c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Nomenclatura c", null);
        }

    }
}