using System.Collections.Generic;
using System.Collections;
using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IQueryDao : IGenericDao<Object, long>
    {
        List<object[]> query(string strquery, IList parametros);
    }
}
