using System.Collections.Generic;
using System.Collections;
using System;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;

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