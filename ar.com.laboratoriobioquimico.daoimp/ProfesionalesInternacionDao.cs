using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ProfesionalesInternacionDao: CatalogDao<ProfesionalesInternacion, string>, IProfesionalesInternacionDao
    {
        
        public IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from ProfesionalesInternacion c order by c.Nombre", pageNumber, pageSize);
        }

        public IList<ProfesionalesInternacion> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from ProfesionalesInternacion c where c.Nombre like " + "'" + find + "%' order by c.Nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from ProfesionalesInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from ProfesionalesInternacion c", null);
        }

    }
}