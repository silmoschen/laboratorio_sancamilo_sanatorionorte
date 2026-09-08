namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public abstract class CatalogDao<T, L> : GenericDao<T, L> where T : class
    {
        protected override bool UseQueryCache
        {
            get { return true; }
        }
    }
}
