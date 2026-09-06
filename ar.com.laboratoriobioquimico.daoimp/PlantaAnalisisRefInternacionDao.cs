using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PlantaAnalisisRefInternacionDao : GenericDao<PlantaAnalisisRefInternacion, PlantaAnalisisRefInternacionPK>, IPlantaAnalisisRefInternacionDao
    {
        
        public IList<PlantaAnalisisRefInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from PlantaAnalisisRefInternacion c order by c.ID.codigo, c.ID.items", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from PlantaAnalisisRefInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from PlantaAnalisisRefInternacion c", null);
        }

    }
}