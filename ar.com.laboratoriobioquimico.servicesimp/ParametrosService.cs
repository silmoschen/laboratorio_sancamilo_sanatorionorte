using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ParametrosService: IParametrosService
    {
        private IParametrosDao entityDao { get; set; }

        public Parametros find(int id)
        {
            return entityDao.get(id);
        }

        public void persist(Parametros entity)
        {
            entityDao.persist(entity);
        }

        public string remove(Parametros entity)
        {
            return entityDao.remove(entity);
        }
      
    }
}