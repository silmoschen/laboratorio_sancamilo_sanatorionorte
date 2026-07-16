using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PlantaAnalisisRefService: IPlantaAnalisisRefService
    {
        private IPlantaAnalisisRefDao entityDao { get; set; }

        public PlantaAnalisisRef find(PlantaAnalisisRefPK id)
        {
            return entityDao.get(id);
        }

        public void persist(PlantaAnalisisRef entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(PlantaAnalisisRef entity)
        {
            return entityDao.remove(entity);
        }

        public IList<PlantaAnalisisRef> getAll(int pageNumber, int pageSize)
        {
            return entityDao.getAll(pageNumber, pageSize);
        }


        public long getMaxPage(int pageSize)
        {
            return entityDao.getMaxPage(pageSize);
        }

        public long getTotalRegistros()
        {
            return entityDao.getTotalRegistros();
        }

        public void updateBatch(IList<PlantaAnalisisRef> entities)
        {
            entityDao.persistBatch(entities);
        }

        public void deleteBatch(IList<PlantaAnalisisRef> entities)
        {
            foreach (PlantaAnalisisRef c in entities)
                if (entityDao.get(c.ID) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}