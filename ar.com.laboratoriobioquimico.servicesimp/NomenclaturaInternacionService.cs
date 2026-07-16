using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class NomenclaturaInternacionService: INomenclaturaInternacionService
    {
        private INomenclaturaInternacionDao entityDao { get; set; }        
        
        public NomenclaturaInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(NomenclaturaInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(NomenclaturaInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<NomenclaturaInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<NomenclaturaInternacion> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(List<NomenclaturaInternacion> entities)
        {
            foreach (NomenclaturaInternacion c in entities)
                if (entityDao.get(c.codigo) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}