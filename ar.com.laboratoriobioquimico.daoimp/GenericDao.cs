using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using NHibernate;
using NHibernate.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public abstract class GenericDao<T, L> : IGenericDao<T, L> where T : class
    {        

        private int batchSize = 50;

        private ISessionFactory sessionFactory;

        public ISessionFactory SessionFactory
        {
            protected get { return sessionFactory; }
            set { sessionFactory = value; }
        }

        protected ISession session
        {
            get { return sessionFactory.GetCurrentSession(); }
        }

        public T get(L id)
        {
            try
            {
                return session.Get<T>(id);
            }
            catch (HibernateException e)
            {
                throw;
            }
        }

        public async Task<T> GetAsync<T>(L id)
        {
            return await session.GetAsync<T>(id);
        }

        public T get(string strquery, IList l)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetFirstResult(0);
                query.SetMaxResults(1);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                if (l != null)
                    for (int i = 0; i < l.Count; i++) query.SetParameter("p" + Convert.ToString(i), l[i]);
                query.List(lista);
                if (lista.Count == 0) return null; else return lista[0];
            }
            catch (HibernateException e)
            {
                throw;
            }
        }

        public T load(L id)
        {
            try
            {
                return this.session.Load<T>(id);
            }
            catch (HibernateException e)
            {
                return null;
            }
        }


        public string persist(T entity)
        {
            string ex = null;

            try
            {
                session.SaveOrUpdate(entity);                
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Insertar el Registro: " + e.Message;
            }

            return ex;
        }


        public T persistID(T entity)
        {
            try
            {
                T pojo = (T)session.Save(entity);                
                return pojo;
            }
            catch (GenericADOException e)
            {
                return null;

            }
        }


        public string save(T entity)
        {
            string ex = null;

            try
            {
                session.Save(entity);
            }
            catch (GenericADOException e)
            {

                ex = "Se ha Producido un Error al intentar Insertar el Registro: " + e.Message;
            }

            return ex;
        }


        public string update(T entity)
        {
            string ex = null;

            try
            {
                session.Update(entity);                
            }
            catch (GenericADOException e)
            {

                ex = "Se ha Producido un Error al intentar Actualizar el Registro: " + e.Message;
            }


            return ex;
        }

        public string merge(T entity)
        {
            string ex = null;

            try
            {
                session.Merge(entity);
                session.Evict(entity);
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Actualizar el Registro: " + e.Message;                
            }

            return ex;
        }

        public string remove(T entity)
        {
            string ex = null;

            try
            {
                session.Delete(entity);                               
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Insertar el Registro: " + e.Message;
            }

            return ex;
        }

        public string persistBatch(IList<T> list)
        {
            string ex = null;

            session.CacheMode = CacheMode.Ignore;

            try
            {
                for (int i = 1; i <= list.Count; i++)
                {
                    session.SaveOrUpdate(list[i - 1]);
                    
                    if (i % batchSize == 0 && i > 0)
                    {
                        session.Flush();
                        session.Clear();
                    }
                    
                }

                session.Flush();
                session.Clear();
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Borrar el Registro: " + e.Message;
            }

            session.CacheMode = CacheMode.Normal;
            list.Clear();
         
            return ex;
        }

        public string updateBatch(IList<T> list)
        {
            string ex = null;

            session.CacheMode = CacheMode.Ignore;

            try
            {
                for (int i = 1; i <= list.Count; i++)
                {
                    session.Update(list[i - 1]);

                    if (i % batchSize == 0 && i > 0)
                    {
                        session.Flush();
                        session.Clear();
                    }
                }

                session.Flush();
                session.Clear();
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Borrar el Registro: " + e.Message;
            }

            session.CacheMode = CacheMode.Normal;
            list.Clear();
            list = null;

            return ex;
        }

        public string removeBatch(IList<T> list)
        {
            string ex = null;

            session.CacheMode = CacheMode.Ignore;

            try
            {
                for (int i = 1; i <= list.Count; i++)
                {
                    session.Delete(list[i - 1]);

                    if (i % batchSize == 0 && i > 0)
                    {
                        session.Flush();
                    }
                }

                session.Flush();
                session.Clear();
            }
            catch (GenericADOException e)
            {
                ex = "Se ha Producido un Error al intentar Borrar el Registro: " + e.Message;
            }

            session.CacheMode = CacheMode.Normal;
            list.Clear();
            list = null;

            return ex;
        }
        public IList<T> getAll(T entity)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery("from " + entity.GetType());
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IList<T> getAll(string strquery)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<T> getAll(string strquery, int? pageNumber, int? pageSize)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                if (pageNumber != null && pageSize != null)
                {
                    if (pageNumber.Value > 0) query.SetFirstResult((pageNumber.Value - 1) * pageSize.Value);
                    if (pageSize.Value > 0) query.SetMaxResults(pageSize.Value);
                }
                query.List(lista);
                return lista;
            }
            catch (HibernateException e)
            {
                throw;
            }
        }

        public IList<T> getAll(string strquery, IList l, int pageNumber, int pageSize)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                for (int i = 0; i < l.Count; i++) query.SetParameter("p" + Convert.ToString(i), l[i]);
                if (pageNumber > 0) query.SetFirstResult((pageNumber - 1) * pageSize);
                if (pageSize > 0) query.SetMaxResults(pageSize);
                query.List(lista);
                return lista;
            }
            catch (HibernateException e)
            {
                throw;
            }
        }

        public IList<T> getAll(string strquery, IList l)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                for (int i = 0; i < l.Count; i++) query.SetParameter("p" + Convert.ToString(i), l[i]);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<T> getListEntities(string strquery)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<T> getListEntities(string strquery, IList l)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                for (int i = 0; i < l.Count; i++) query.SetParameter("p" + Convert.ToString(i), l[i]);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<T> getListEntities(string strquery, IList l, int pageSize)
        {
            try
            {
                List<T> lista = new List<T>();
                IQuery query = session.CreateQuery(strquery);
                query.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                if (l != null)
                    for (int i = 0; i < l.Count; i++) query.SetParameter("p" + Convert.ToString(i), l[i]);
                query.SetFirstResult(0);
                query.SetMaxResults(pageSize);
                query.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object[]> getListObjects(string query, IList l)
        {
            try
            {
                List<object[]> lista = new List<object[]>();
                IQuery q = session.CreateSQLQuery(query);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                q.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long getMaxPage(string strquery, int pageSize)
        {
            try
            {
                IQuery q = session.CreateQuery(strquery);
                q.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                Int64 iss = q.UniqueResult<Int64>();
                return (iss / pageSize) + 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long getMaxPage(string strquery, IList l, int pageSize)
        {
            try
            {
                IQuery q = session.CreateQuery(strquery);
                q.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                Int64 iss = q.UniqueResult<Int64>();
                return (iss / pageSize) + 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long getTotalRegistros(string strquery, IList l)
        {
            try
            {
                IQuery q = session.CreateQuery(strquery);
                q.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                Int64 iss = q.UniqueResult<Int64>();
                return iss;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long getResultLong(string strquery)
        {
            try
            {
                IQuery q = session.CreateQuery(strquery);
                q.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                Int64 r = q.UniqueResult<Int64>();
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long getResultLong(string strquery, IList l)
        {
            try
            {
                IQuery q = session.CreateQuery(strquery);
                q.SetCacheable(true).SetFlushMode(FlushMode.Auto);
                for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                Int64 r = q.UniqueResult<Int64>();
                return r;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void queryUpdate(string query, IList l)
        {
            try
            {
                IQuery q = session.CreateQuery(query);
                for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                q.ExecuteUpdate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void queryNativeSQL(string query, IList l)
        {
            try
            {
                IQuery q = session.CreateSQLQuery(query);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                q.ExecuteUpdate();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object[]> getListNativeSQL(string query, IList l)
        {
            try
            {
                List<object[]> lista = new List<object[]>();
                IQuery q = session.CreateSQLQuery(query);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                q.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object[]> getListNativeSQL(string query, int pageNumber, int pageSize)
        {
            try
            {
                List<object[]> lista = new List<object[]>();
                IQuery q = session.CreateSQLQuery(query);
                q.SetFirstResult(0);
                q.SetMaxResults(pageSize);
                q.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Object getNativeSQL(string query, IList l)
        {
            try
            {
                IQuery q = session.CreateSQLQuery(query);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);

                return q.UniqueResult();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<object[]> getListHQLObjects(string query, IList l)
        {
            try
            {
                List<object[]> lista = new List<object[]>();
                IQuery q = session.CreateQuery(query);
                if (l != null) for (int i = 0; i < l.Count; i++) q.SetParameter("p" + Convert.ToString(i), l[i]);
                q.List(lista);
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }        
    }
}