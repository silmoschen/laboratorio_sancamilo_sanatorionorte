using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPlantaAnalisisRefService
    {
        PlantaAnalisisRef find(PlantaAnalisisRefPK id);
        void persist(PlantaAnalisisRef entity);
        string remove(PlantaAnalisisRef entity);
        IList<PlantaAnalisisRef> getAll(int pageNumber, int pageSize);
        
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<PlantaAnalisisRef> entities);
        void deleteBatch(IList<PlantaAnalisisRef> entities);
    }
}
