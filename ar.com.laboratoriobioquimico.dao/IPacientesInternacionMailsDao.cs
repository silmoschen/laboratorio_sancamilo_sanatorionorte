using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPacientesInternacionMailsDao : IGenericDao<PacientesInternacionMails, string>
    {
        PacientesInternacionMails get(PacientesInternacion entity);        
    }
}
