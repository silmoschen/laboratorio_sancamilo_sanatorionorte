using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPlantaAnalisisService
    {
        PlantaAnalisis find(PlantaAnalisisPK id);
        void persist(PlantaAnalisis entity);
        string remove(PlantaAnalisis entity);
        IList<PlantaAnalisis> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisis> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisis> getItems(string codigo);

        void updateBatch(IList<PlantaAnalisis> entities);
        void deleteBatch(IList<PlantaAnalisis> entities);
    }
}
