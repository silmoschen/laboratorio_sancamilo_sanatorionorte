using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ProfesionalesService: IProfesionalesService
    {
        private IProfesionalesDao entityDao { get; set; }        
        
        public Profesionales find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(Profesionales entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(Profesionales entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Profesionales> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Profesionales> getList(String find, int pageNumber, int pageSize)
        {
            return entityDao.getList(find, pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

        public void updateBatch(IList<Profesionales> entities)
        {
            foreach (Profesionales c in entities)
                if (entityDao.get(c.idprof) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}