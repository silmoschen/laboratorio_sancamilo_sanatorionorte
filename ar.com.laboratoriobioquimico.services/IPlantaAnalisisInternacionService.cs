using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IPlantaAnalisisInternacionService
    {
        PlantaAnalisisInternacion find(PlantaAnalisisInternacionPK id);
        void persist(PlantaAnalisisInternacion entity);
        string remove(PlantaAnalisisInternacion entity);
        IList<PlantaAnalisisInternacion> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisisInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisisInternacion> getItems(string codigo);

        void updateBatch(IList<PlantaAnalisisInternacion> entities);
        void deleteBatch(IList<PlantaAnalisisInternacion> entities);
    }
}
