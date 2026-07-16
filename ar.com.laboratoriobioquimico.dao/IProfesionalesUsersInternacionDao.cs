using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IProfesionalesUsersInternacionDao : IGenericDao<ProfesionalesUsersInternacion, string>
    {
        ProfesionalesUsersInternacion getUser(string user, string pass);        
    }
}
