using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisRefInternacionDao : IGenericDao<PlantaAnalisisRefInternacion, PlantaAnalisisRefInternacionPK>
    {
        IList<PlantaAnalisisRefInternacion> getAll(int pageNumber, int pageSize);
        
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
