using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ProfesionalesInternacionService: IProfesionalesInternacionService
    {
        private IProfesionalesInternacionDao entityDao { get; set; }        
        
        public ProfesionalesInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(ProfesionalesInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(ProfesionalesInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<ProfesionalesInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<ProfesionalesInternacion> getList(string find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<ProfesionalesInternacion> entities)
        {
            foreach (ProfesionalesInternacion c in entities)
                if (entityDao.get(c.idprof) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}