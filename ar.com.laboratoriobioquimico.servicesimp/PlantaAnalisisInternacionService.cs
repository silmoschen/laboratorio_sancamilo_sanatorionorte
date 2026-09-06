using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PlantaAnalisisInternacionService: IPlantaAnalisisInternacionService
    {
        private IPlantaAnalisisInternacionDao entityDao { get; set; }

        public PlantaAnalisisInternacion find(PlantaAnalisisInternacionPK id)
        {
            return entityDao.get(id);
        }

        public void persist(PlantaAnalisisInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(PlantaAnalisisInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<PlantaAnalisisInternacion> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<PlantaAnalisisInternacion> getList(string find, int pageNumber, int pageSize)
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

        public IList<PlantaAnalisisInternacion> getItems(string codigo)
        {
            return entityDao.getItems(codigo);
        }

        public void updateBatch(IList<PlantaAnalisisInternacion> entities)
        {        
            foreach (PlantaAnalisisInternacion c in entities)
                if (entityDao.get(c.ID) == null) entityDao.save(c); else entityDao.merge(c);
        }

        public void deleteBatch(IList<PlantaAnalisisInternacion> entities)
        {
            entityDao.removeBatch(entities);
        }
    }
}