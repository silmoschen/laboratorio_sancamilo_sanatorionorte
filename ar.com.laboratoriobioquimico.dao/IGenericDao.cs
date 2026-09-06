using System.Collections.Generic;
using System.Collections;
using System.Threading.Tasks;
using System;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IGenericDao<T, L>
    {
        T get(L id);
        Task<T> GetAsync<T>(L id);
        T get(string strquery, IList l);
        T load(L id);
        string persist(T entity);
        T persistID(T entity);
        string save(T entity);
        string update(T entity);
        string merge(T entity);
        string remove(T entity);
        string persistBatch(IList<T> list);
        string updateBatch(IList<T> list);
        string removeBatch(IList<T> list);
        IList<T> getAll(string strquery);
        IList<T> getAll(T entity);
        IList<T> getAll(string strquery, int? pageNumber, int? pageSize);
        IList<T> getAll(string strquery, IList l, int pageNumber, int pageSize);
        IList<T> getListEntities(string strquery);
        IList<T> getListEntities(string strquery, IList l);
        IList<T> getListEntities(string strquery, IList l, int pageSize);
        long getMaxPage(string strquery, int pageSize);
        long getMaxPage(string strquery, IList l, int pageSize);
        long getTotalRegistros(string strquery, IList l);
        long getResultLong(string strquery);
        long getResultLong(string strquery, IList l);
        void queryUpdate(string query, IList l);
        void queryNativeSQL(string query, IList l);
        Object getNativeSQL(string query, IList l);
        List<object[]> getListNativeSQL(string query, IList l);
        List<object[]> getListNativeSQL(string query, int pageNumber, int pageSize);
        List<object[]> getListHQLObjects(string query, IList l);        
    }
}