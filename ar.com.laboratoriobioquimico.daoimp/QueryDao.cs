using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using System;
using System.Collections;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class QueryDao : GenericDao<Object, long>, IQueryDao
    {
        public List<object[]> query(string strquery, IList parametros)
        {
            return getListNativeSQL(strquery, parametros);
        }
    }
}