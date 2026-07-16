using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IParametrosService
    {
        Parametros find(int id);
        void persist(Parametros entity);
        string remove(Parametros entity);        
    }
}
