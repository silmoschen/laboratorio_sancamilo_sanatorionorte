using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class LogsService : ILogsService
    {
        private ILogsDao entityDao { get; set; }

        public Logs find(string id)
        {
            return entityDao.get(id);
        }

        public void persist(Logs entity)
        {
            entityDao.save(entity);
        }

        public string remove(Logs entity)
        {
            return entityDao.remove(entity);
        }

        public IList<Logs> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<Logs> getList(String find, int pageNumber, int pageSize)
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