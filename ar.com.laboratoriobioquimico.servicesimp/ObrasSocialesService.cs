using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class ObrasSocialesService: IObrasSocialesService
    {
        private IObrasSocialesDao entityDao { get; set; }        
        
        public ObrasSociales find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(ObrasSociales entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(ObrasSociales entity)
        {
            return entityDao.remove(entity);
        }

        public IList<ObrasSociales> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<ObrasSociales> getList(String find, int pageNumber, int pageSize)
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

        public void updateBatch(IList<ObrasSociales> entities)
        {
            foreach (ObrasSociales c in entities)
                if (entityDao.get(c.codos) == null) entityDao.save(c); else entityDao.merge(c);
        }
    }
}