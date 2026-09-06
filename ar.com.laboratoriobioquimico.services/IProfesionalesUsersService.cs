using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IProfesionalesUsersService
    {
        ProfesionalesUsers find(string id);
        void persist(ProfesionalesUsers entity);

        void update(ProfesionalesUsers entity);
        string remove(ProfesionalesUsers entity);
        ProfesionalesUsers findUser(string user, string pass);
        void updateBatch(IList<ProfesionalesUsers> entities);
    }
}
