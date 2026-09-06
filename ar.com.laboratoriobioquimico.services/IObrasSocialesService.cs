using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IObrasSocialesService
    {
        ObrasSociales find(string id);
        void persist(ObrasSociales entity);
        string remove(ObrasSociales entity);
        IList<ObrasSociales> getAll(int pageNumber, int pageSize);
        IList<ObrasSociales> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<ObrasSociales> entities);
    }
}
