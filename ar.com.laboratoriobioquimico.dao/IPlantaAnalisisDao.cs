using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisDao : IGenericDao<PlantaAnalisis, PlantaAnalisisPK>
    {
        IList<PlantaAnalisis> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisis> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisis> getItems(string codigo);
    }
}
