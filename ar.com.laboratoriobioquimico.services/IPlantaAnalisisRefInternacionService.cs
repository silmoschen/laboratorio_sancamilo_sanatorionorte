using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPlantaAnalisisRefInternacionService
    {
        PlantaAnalisisRefInternacion find(PlantaAnalisisRefInternacionPK id);
        void persist(PlantaAnalisisRefInternacion entity);
        string remove(PlantaAnalisisRefInternacion entity);
        IList<PlantaAnalisisRefInternacion> getAll(int pageNumber, int pageSize);
        
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<PlantaAnalisisRefInternacion> entities);
        void deleteBatch(IList<PlantaAnalisisRefInternacion> entities);
    }
}
