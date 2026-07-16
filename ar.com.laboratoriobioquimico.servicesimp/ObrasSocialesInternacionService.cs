using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ObrasSocialesInternacionService: IObrasSocialesInternacionService
    {
        private IObrasSocialesInternacionDao entityDao { get; set; }        
        
        public ObrasSocialesInternacion find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(ObrasSocialesInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(ObrasSocialesInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<ObrasSocialesInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<ObrasSocialesInternacion> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<ObrasSocialesInternacion> entities)
        {
            foreach (ObrasSocialesInternacion c in entities)
                if (entityDao.get(c.codos) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}