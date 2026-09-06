using System.Collections.Generic;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class LogsDao : GenericDao<Logs, string>, ILogsDao
    {

        public IList<Logs> getAll(int pageNumber, int pageSize)
        {
            return getAll("from Logs c order by c.Fechahora desc", pageNumber, pageSize);
        }

        public IList<Logs> getList(string find, int pageNumber, int pageSize)
        {
            return getAll("from Logs c where c.Descrip like " + "'" + find + "%' order by c.Fechahora desc", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Logs c", pageSize);
        }

        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Logs c", null);
        }
    }
}