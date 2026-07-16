using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class PlantaAnalisisInternacionDao : GenericDao<PlantaAnalisisInternacion, PlantaAnalisisInternacionPK>, IPlantaAnalisisInternacionDao
    {
        
        public IList<PlantaAnalisisInternacion> getAll(int pageNumber, int pageSize)
        {    
            return getAll("from PlantaAnalisisInternacion c order by c.ID.codigo, c.ID.items", pageNumber, pageSize);
        }

        public IList<PlantaAnalisisInternacion> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from PlantaAnalisisInternacion c where c.ID.codigo like " + "'" + find + "%' order by c.ID.codigo", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from PlantaAnalisisInternacion c", pageSize); 
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from PlantaAnalisisInternacion c", null);
        }

        public IList<PlantaAnalisisInternacion> getItems(string codigo)
        {
            object[] l = { codigo };
            return getAll("from PlantaAnalisisInternacion c where c.ID.codigo = :p0 order by c.ID.items", l);
        }

    }
}