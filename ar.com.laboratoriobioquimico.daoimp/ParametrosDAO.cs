using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class ParametrosDao: CatalogDao<Parametros, int>, IParametrosDao
    {
        protected override string QueryCacheRegion
        {
            get { return "ParametrosQueries"; }
        }
    }
}