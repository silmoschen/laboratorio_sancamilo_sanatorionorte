using System.Collections.Generic;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisRefInternacionDao : IGenericDao<PlantaAnalisisRefInternacion, PlantaAnalisisRefInternacionPK>
    {
        IList<PlantaAnalisisRefInternacion> getAll(int pageNumber, int pageSize);
        
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
