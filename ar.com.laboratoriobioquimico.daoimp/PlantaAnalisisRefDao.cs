using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PlantaAnalisisRefDao : GenericDao<PlantaAnalisisRef, PlantaAnalisisRefPK>, IPlantaAnalisisRefDao
    {
        
        public IList<PlantaAnalisisRef> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from PlantaAnalisisRef c order by c.ID.codigo, c.ID.items", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from PlantaAnalisisRef c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from PlantaAnalisisRef c", null);
        }

    }
}