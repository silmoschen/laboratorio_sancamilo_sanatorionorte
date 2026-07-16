using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ProfesionalesUsersService: IProfesionalesUsersService
    {
        private IProfesionalesUsersDao entityDao { get; set; }        
        
        public ProfesionalesUsers find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(ProfesionalesUsers entity)
        {
            if (find(entity.idprof) == null) entityDao.save(entity); else entityDao.merge(entity);            
        }

        public void update(ProfesionalesUsers entity)
        {
            entityDao.update(entity);
        }

        public string remove(ProfesionalesUsers entity)
        {
            return entityDao.remove(entity);
        }

        public ProfesionalesUsers findUser(string user, string pass)
        {
            return entityDao.getUser(user, pass);
        }

        public void updateBatch(IList<ProfesionalesUsers> entities)
        {
            foreach (ProfesionalesUsers c in entities)
                if (entityDao.get(c.idprof) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}