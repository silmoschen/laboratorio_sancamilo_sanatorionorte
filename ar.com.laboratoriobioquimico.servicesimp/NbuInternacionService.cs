using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class NbuInternacionService : INbuInternacionService
    {
        private INbuInternacionDao entityDao { get; set; }

        public NbuInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(NbuInternacion entity)
        {
            entityDao.persist(entity);
        }

        public string remove(NbuInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<NbuInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<NbuInternacion> getList(string find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<NbuInternacion> entities)
        {            
            foreach (NbuInternacion c in entities)
                if (entityDao.get(c.codigo) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}