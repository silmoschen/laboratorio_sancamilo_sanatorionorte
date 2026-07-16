using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PacientesMailsDao: GenericDao<PacientesMails, string>, IPacientesMailsDao
    {

        public PacientesMails get(Pacientes entity)
        {
            object[] l = { entity };
            return get("from PacientesMails c where c.paciente = :p0", l);
        }
    }
}