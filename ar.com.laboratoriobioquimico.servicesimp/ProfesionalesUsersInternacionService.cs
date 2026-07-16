using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ProfesionalesUsersInternacionService: IProfesionalesUsersInternacionService
    {
        private IProfesionalesUsersInternacionDao entityDao { get; set; }        
        
        public ProfesionalesUsersInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(ProfesionalesUsersInternacion entity)
        {
            if (find(entity.idprof) == null) entityDao.save(entity); else entityDao.merge(entity);            
        }

        public string remove(ProfesionalesUsersInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public ProfesionalesUsersInternacion findUser(string user, string pass)
        {
            return entityDao.getUser(user, pass);
        }

        public void updateBatch(IList<ProfesionalesUsersInternacion> entities)
        {
            foreach (ProfesionalesUsersInternacion c in entities)
                if (entityDao.get(c.idprof) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}