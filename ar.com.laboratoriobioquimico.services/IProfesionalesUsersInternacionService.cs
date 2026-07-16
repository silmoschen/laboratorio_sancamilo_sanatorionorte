using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesUsersInternacionService
    {
        ProfesionalesUsersInternacion find(string id);
        void persist(ProfesionalesUsersInternacion entity);
        string remove(ProfesionalesUsersInternacion entity);
        ProfesionalesUsersInternacion findUser(string user, string pass);

        void updateBatch(IList<ProfesionalesUsersInternacion> entities);
    }
}
