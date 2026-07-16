
using laboratoriobioquimico.ar.com.laboratoriobioquimico.dao;
using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.daoimp
{
    public class UsersDao : GenericDao<Users, long>, IUsersDao
    {

        public IList<Users> getAll(int pageNumber, int pageSize)
        {
            return getAll("from Users c order by c.Usuario", pageNumber, pageSize);
        }

        public IList<Users> getList(String find, int pageNumber, int pageSize)
        {
            return getAll("from Users c where c.Usuario like " + "'" + find + "%' order by c.Usuario", pageNumber, pageSize);
        }

        public long getMaxPage(int pageSize)
        {
            return getMaxPage("select count(*) from Users c", pageSize);
        }       

        public Users findUser(string us, string pas)
        {
            object[] l = { us, pas };
            return get("from Users c where c.Usuario = :p0 and c.Pass = :p1", l);
        }

         public Users findUser(string us)
        {
            object[] l = { us };
            return get("from Users c where c.Usuario = :p0", l);
        }

         public IList<Users> getListUsers(String campo, String valor, String orden)
         {
             object[] l = { "%" + valor + "%" };
             return getAll("from Users c where c." + campo + " like :p0 order by c." + orden, l, 0, 10000);
         }  
        
        public long getTotalRegistros()
        {
            return getTotalRegistros("select count(*) from Users c", null);
        }
    }
}