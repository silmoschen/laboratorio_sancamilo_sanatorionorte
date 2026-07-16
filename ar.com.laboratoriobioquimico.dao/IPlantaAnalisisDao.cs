using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IPlantaAnalisisDao : IGenericDao<PlantaAnalisis, PlantaAnalisisPK>
    {
        IList<PlantaAnalisis> getAll(int pageNumber, int pageSize);
        IList<PlantaAnalisis> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        IList<PlantaAnalisis> getItems(string codigo);
    }
}
