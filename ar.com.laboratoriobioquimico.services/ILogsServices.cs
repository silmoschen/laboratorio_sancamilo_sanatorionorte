using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface ILogsService
    {
        Logs find(string id);
        void persist(Logs entity);
        string remove(Logs entity);
        IList<Logs> getAll(int pageNumber, int pageSize);
        IList<Logs> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
