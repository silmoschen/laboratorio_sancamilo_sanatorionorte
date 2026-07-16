using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IObrasSocialesService
    {
        ObrasSociales find(string id);
        void persist(ObrasSociales entity);
        string remove(ObrasSociales entity);
        IList<ObrasSociales> getAll(int pageNumber, int pageSize);
        IList<ObrasSociales> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<ObrasSociales> entities);
    }
}
