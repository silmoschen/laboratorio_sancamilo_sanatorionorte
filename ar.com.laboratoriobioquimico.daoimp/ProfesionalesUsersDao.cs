using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ProfesionalesUsersDao: CatalogDao<ProfesionalesUsers, string>, IProfesionalesUsersDao
    {
        public ProfesionalesUsers getUser(string user, string pass)
        {
            object[] l = { user, pass };
            return get("from ProfesionalesUsers c where c.user = :p0 and c.pass = :p1", l);
        }
    }
}