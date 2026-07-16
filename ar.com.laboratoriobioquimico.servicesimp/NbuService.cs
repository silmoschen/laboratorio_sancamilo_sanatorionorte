using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class NbuService : INbuService
    {
        private INbuDao entityDao { get; set; }

        public Nbu find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(Nbu entity)
        {
            entityDao.persist(entity);
        }

        public string remove(Nbu entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Nbu> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Nbu> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<Nbu> entities)
        {            
            foreach (Nbu c in entities)
                if (entityDao.get(c.codigo) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}