using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System.Collections.Generic;

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
