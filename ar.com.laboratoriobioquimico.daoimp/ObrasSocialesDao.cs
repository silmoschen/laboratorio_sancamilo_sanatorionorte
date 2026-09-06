using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ObrasSocialesDao: GenericDao<ObrasSociales, string>, IObrasSocialesDao
    {
        
        public IList<ObrasSociales> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from ObrasSociales c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<ObrasSociales> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from ObrasSociales c where c.Nombre like " + "'" + find + "%' order by c.Nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from ObrasSociales c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from ObrasSociales c", null);
        }

    }
}