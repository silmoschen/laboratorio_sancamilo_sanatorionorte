using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.services
{
    public interface ILogsService
    {
        Logs find(string id);
        void persist(Logs entity);
        string remove(Logs entity);
        IList<Logs> getAll(int pageNumber, int pageSize);
        IList<Logs> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
