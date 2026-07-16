using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class RolesService: IRolesService
    {
        private IRolesDao entityDao { get; set; }        
        
        public Roles find(long id)
        {          
            return entityDao.get(id);
        }

        public void persist(Roles entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(Roles entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Roles> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Roles> getList(String find, int pageNumber, int pageSize)
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
    }
}