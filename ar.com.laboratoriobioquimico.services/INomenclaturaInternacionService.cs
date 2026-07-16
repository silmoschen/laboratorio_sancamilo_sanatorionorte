using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface INomenclaturaInternacionService
    {
        NomenclaturaInternacion find(string id);
        void persist(NomenclaturaInternacion entity);
        string remove(NomenclaturaInternacion entity);
        IList<NomenclaturaInternacion> getAll(int pageNumber, int pageSize);
        IList<NomenclaturaInternacion> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();

        void updateBatch(List<NomenclaturaInternacion> entities);
    }
}
