using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPlantaAnalisisService
    {
        PlantaAnalisis find(PlantaAnalisisPK id);
        void persist(PlantaAnalisis entity);
        string remove(PlantaAnalisis entity);
        IList<PlantaAnalisis> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisis> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisis> getItems(string codigo);

        void updateBatch(IList<PlantaAnalisis> entities);
        void deleteBatch(IList<PlantaAnalisis> entities);
    }
}
