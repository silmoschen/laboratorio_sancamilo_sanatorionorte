using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisInternacionDao : IGenericDao<PlantaAnalisisInternacion, PlantaAnalisisInternacionPK>
    {
        IList<PlantaAnalisisInternacion> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisisInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisisInternacion> getItems(string codigo);
    }
}
