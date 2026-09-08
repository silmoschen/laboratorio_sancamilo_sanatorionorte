using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ProfesionalesDao: CatalogDao<Profesionales, string>, IProfesionalesDao
    {
        
        public IList<Profesionales> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from Profesionales c order by c.Nombre", pageNumber, pageSize);
        }

        public IList<Profesionales> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from Profesionales c where c.Nombre like " + "'" + find + "%' order by c.Nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Profesionales c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Profesionales c", null);
        }

    }
}