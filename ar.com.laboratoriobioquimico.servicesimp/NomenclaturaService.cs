using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class NomenclaturaService: INomenclaturaService
    {
        private INomenclaturaDao entityDao { get; set; }        
        
        public Nomenclatura find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(Nomenclatura entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(Nomenclatura entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Nomenclatura> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Nomenclatura> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<Nomenclatura> entities)
        {
            foreach (Nomenclatura c in entities)
                if (entityDao.get(c.codigo) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}