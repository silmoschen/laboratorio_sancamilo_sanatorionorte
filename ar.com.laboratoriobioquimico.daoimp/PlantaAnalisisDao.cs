using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PlantaAnalisisDao : GenericDao<PlantaAnalisis, PlantaAnalisisPK>, IPlantaAnalisisDao
    {
        
        public IList<PlantaAnalisis> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from PlantaAnalisis c order by c.ID.codigo, c.ID.items", pageNumber, pageSize);
        }

        public IList<PlantaAnalisis> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from PlantaAnalisis c where c.ID.codigo like " + "'" + find + "%' order by c.ID.codigo", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from PlantaAnalisis c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from PlantaAnalisis c", null);
        }

        public IList<PlantaAnalisis> getItems(string codigo)
        {
            object[] l = { codigo };
            return getAll("from PlantaAnalisis c where c.ID.codigo = :p0 order by c.ID.items", l);
        }

    }
}