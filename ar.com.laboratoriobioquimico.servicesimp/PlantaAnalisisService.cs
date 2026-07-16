using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PlantaAnalisisService: IPlantaAnalisisService
    {
        private IPlantaAnalisisDao entityDao { get; set; }

        public PlantaAnalisis find(PlantaAnalisisPK id)
        {
            return entityDao.get(id);
        }

        public void persist(PlantaAnalisis entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(PlantaAnalisis entity)
        {
            return entityDao.remove(entity);
        }

        public IList<PlantaAnalisis> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }

        public IList<PlantaAnalisis> getList(String find, int pageNumber, int pageSize)
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

        public IList<PlantaAnalisis> getItems(string codigo)
        {
            return entityDao.getItems(codigo);
        }

        public void updateBatch(IList<PlantaAnalisis> entities)
        {        
            foreach (PlantaAnalisis c in entities)
                if (entityDao.get(c.ID) == null) entityDao.save(c); else entityDao.merge(c);
        }

        public void deleteBatch(IList<PlantaAnalisis> entities)
        {
            entityDao.removeBatch(entities);
        }
    }
}