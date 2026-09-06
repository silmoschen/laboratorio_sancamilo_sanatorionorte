using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface ILogsDao : IGenericDao<Logs, string>
    {
        IList<Logs> getAll(int pageNumber, int pageSize);
        IList<Logs> getList(string find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);
        long getTotalRegistros();
    }
}
