using laboratoriobioquimico.ar.com.laboratoriobioquimico.entities;
using System;
using System.Collections.Generic;

namespace laboratoriobioquimico.ar.com.laboratoriobioquimico.dao
{
    public interface IUsersDao : IGenericDao<Users, long>
    {
        IList<Users> getAll(int pageNumber, int pageSize);
        IList<Users> getList(String find, int pageNumber, int pageSize);
        long getMaxPage(int pageSize);        
        Users findUser(string us, string pas);
        Users findUser(string us);
        IList<Users> getListUsers(String campo, String valor, String orden);
        long getTotalRegistros();
    }
}
