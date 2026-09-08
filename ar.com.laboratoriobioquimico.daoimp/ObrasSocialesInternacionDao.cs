using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ObrasSocialesInternacionDao: CatalogDao<ObrasSocialesInternacion, string>, IObrasSocialesInternacionDao
    {
        
        public IList<ObrasSocialesInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from ObrasSocialesInternacion c order by c.Descrip", pageNumber, pageSize);
        }

        public IList<ObrasSocialesInternacion> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from ObrasSocialesInternacion c where c.Nombre like " + "'" + find + "%' order by c.Nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from ObrasSocialesInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from ObrasSocialesInternacion c", null);
        }

    }
}