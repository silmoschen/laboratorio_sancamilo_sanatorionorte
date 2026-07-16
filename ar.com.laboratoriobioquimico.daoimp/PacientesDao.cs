using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PacientesDao: GenericDao<Pacientes, string>, IPacientesDao
    {
        
        public IList<Pacientes> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from Pacientes c order by c.nombre", pageNumber, pageSize);
        }

        public IList<Pacientes> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from Pacientes c where c.nombre like " + "'" + find + "%' order by c.nombre", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Pacientes c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Pacientes c", null);
        }

        public Pacientes getByDocumento(string nrodoc)
        {
            object[] l = { nrodoc };
            return get("from Pacientes c where c.nrodoc = :p0", l);

        }

    }
}