using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisInternacionDao : IGenericDao<PlantaAnalisisInternacion, PlantaAnalisisInternacionPK>
    {
        IList<PlantaAnalisisInternacion> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisisInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisisInternacion> getItems(string codigo);
    }
}
