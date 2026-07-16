using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PacientesInternacionDao: GenericDao<PacientesInternacion, string>, IPacientesInternacionDao
    {
        
        public IList<PacientesInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from PacientesInternacion c order by c.nombre", pageNumber, pageSize);
        }

        public IList<PacientesInternacion> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from PacientesInternacion c where c.nombre like " + "'" + find + "%' order by c.nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from PacientesInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from PacientesInternacion c", null);
        }

        public PacientesInternacion getByDocumento(string nrodoc)
        {
            object[] l = { nrodoc };
            return get("from PacientesInternacion c where c.nrodoc = :p0", l);

        }

    }
}