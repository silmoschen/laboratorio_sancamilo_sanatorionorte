using System;
using System.Collections;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IQueryDao : IGenericDao<Object, long>
    {
        List<object[]> query(string strquery, IList parametros);
    }
}
