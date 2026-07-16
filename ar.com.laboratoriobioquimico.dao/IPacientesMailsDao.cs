using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesMailsDao : IGenericDao<PacientesMails, string>
    {
        PacientesMails get(Pacientes entity);        
    }
}
