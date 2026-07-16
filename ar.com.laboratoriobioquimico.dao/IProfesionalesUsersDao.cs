using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesUsersDao : IGenericDao<ProfesionalesUsers, string>
    {
        ProfesionalesUsers getUser(string user, string pass);        
    }
}
