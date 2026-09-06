using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface IObrasSocialesInternacionService
    {
        ObrasSocialesInternacion find(string id);
        void persist(ObrasSocialesInternacion entity);
        string remove(ObrasSocialesInternacion entity);
        IList<ObrasSocialesInternacion> getAll(int pageNumber, int pageSize);
        IList<ObrasSocialesInternacion> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(IList<ObrasSocialesInternacion> entities);
    }
}
