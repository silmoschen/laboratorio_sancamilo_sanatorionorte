using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PacientesInternacionMailsDao: GenericDao<PacientesInternacionMails, string>, IPacientesInternacionMailsDao
    {

        public PacientesInternacionMails get(PacientesInternacion entity)
        {
            object[] l = { entity };
            return get("from PacientesInternacionMails c where c.paciente = :p0", l);
        }
    }
}