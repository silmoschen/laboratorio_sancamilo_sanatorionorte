using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.services;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.servicesimp
{
    public class PlantaAnalisisRefInternacionService: IPlantaAnalisisRefInternacionService
    {
        private IPlantaAnalisisRefInternacionDao entityDao { get; set; }

        public PlantaAnalisisRefInternacion find(PlantaAnalisisRefInternacionPK id)
        {
            return entityDao.get(id);
        }

        public void persist(PlantaAnalisisRefInternacion entity)
        {
            entityDao.persist(entity);            
        }

        public string remove(PlantaAnalisisRefInternacion entity)
        {
            return entityDao.remove(entity);
        }

        public IList<PlantaAnalisisRefInternacion> getAll(int pageNumber, int pageSize)
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

        public void updateBatch(IList<PlantaAnalisisRefInternacion> entities)
        {
            entityDao.persistBatch(entities);
        }

        public void deleteBatch(IList<PlantaAnalisisRefInternacion> entities)
        {
            foreach (PlantaAnalisisRefInternacion c in entities)
                if (entityDao.get(c.ID) == null) entityDao.save(c); else entityDao.merge(c);

        }
    }
}