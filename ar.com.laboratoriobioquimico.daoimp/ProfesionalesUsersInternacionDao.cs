using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ProfesionalesUsersInternacionDao: GenericDao<ProfesionalesUsersInternacion, string>, IProfesionalesUsersInternacionDao
    {
        public ProfesionalesUsersInternacion getUser(string user, string pass)
        {
            object[] l = { user, pass };
            return get("from ProfesionalesUsersInternacion c where c.user = :p0 and c.pass = :p1", l);
        }
    }
}